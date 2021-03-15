import { Component, OnInit, Injector } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { switchMap } from 'rxjs/operators';
import toastr from "toastr";

import { ProdutoServices } from './../../../services/produto/produto.services';
import { Produto } from './../../../models/produtos/produto.model';

@Component({
  selector: 'app-produtoform',
  templateUrl: './produtoform.component.html',
  styleUrls: ['./produtoform.component.css']
})
export class ProdutoformComponent implements OnInit {

  currentAction: string;
  resourceForm: FormGroup;
  pageTitle: string;
  serverErrorMessages: string[] = null;
  submittingForm: boolean = false;
  formData: FormData = new FormData();
  protected route: ActivatedRoute;
  protected router: Router;
  protected formBuilder: FormBuilder;

  public produto: Produto;
  public imagem: string;
  public oldimagem: string;
  public inputFile: boolean = true;
  public imgSemProduto : string = "./../../../../assets/images/semproduto.jpg";

  constructor(
    protected injector: Injector,

    protected produtoService: ProdutoServices
  ) {
    this.route = this.injector.get(ActivatedRoute);
    this.router = this.injector.get(Router);
    this.formBuilder = this.injector.get(FormBuilder);
  }

  ngOnInit() {
    this.setCurrentAction();
    this.buildResourceForm();
    this.loadResource();
  }

  ngAfterContentChecked() {
    this.setPageTitle();
  }

  submitForm() {
    this.submittingForm = true;

    if (this.currentAction == "new")
      this.createProduto();
    else // currentAction == "edit"
      this.updateResource();
  }

  protected setCurrentAction() {
    if (this.route.snapshot.url[0].path == "new")
      this.currentAction = "new"
    else
      this.currentAction = "edit"
  }

  protected loadResource() {
    if (this.currentAction == "edit") {

      this.route.paramMap.pipe(
        switchMap(params => this.produtoService.getById(+params.get("id")))
      )
        .subscribe(
          (resource) => {
            this.produto = resource;
            this.oldimagem = this.imagem = resource.imagem;
         
            if(resource.imagem)
              this.inputFile = false;
            else
            this.imagem = this.imgSemProduto;

            this.resourceForm.patchValue(resource)
          },
          (error) => alert('Ocorreu um erro no servidor, tente mais tarde.')
        )
    }
    else{
      this.imagem = this.imgSemProduto
    }
  }

  protected setPageTitle() {
    if (this.currentAction == 'new')
      this.pageTitle = this.creationPageTitle();
    else {
      this.pageTitle = this.editionPageTitle();
    }
  }

  protected creationPageTitle(): string {
    return "Novo"
  }

  protected editionPageTitle(): string {
    return "Edição"
  }


  public setFiles(event) {
    let files = event.srcElement.files
    if (!files) {
      return
    }

    if (files[0].type != 'image/jpeg' && files[0].type != 'image/jpg' ) {
      event.srcElement.files = null;
      toastr.error("Formato de arquivo inválido!");
      return  
    }

    if (files.length > 0) {
      this.convertToBase64(files[0]);
    }
  }

  private convertToBase64(file): void {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    var self = this;

    reader.onload = function () {
      self.imagem = reader.result.toString();
    };

    reader.onerror = function (error) {
      toastr.error("Ocorreu um erro ao processar a imagem!");
    };
  }


  protected createProduto() {
    const produto: Produto = Object.assign(this.resourceForm.value);
    produto.id = 0;
    produto.imagem = this.imagem != this.imgSemProduto ? this.imagem : null;

    this.produtoService.create(produto)
      .subscribe(
        produto => this.actionsForSuccess(produto),
        error => this.actionsForError(error)
      )
  }

  protected updateResource() {
    const produto: Produto = Object.assign(this.resourceForm.value);
    
    produto.imagem = this.oldimagem != this.imagem ? this.imagem : this.oldimagem;

    if( this.imagem == this.imgSemProduto)
      produto.imagem = null;
    
    this.produtoService.update(produto)
      .subscribe(
        produto => this.actionsForSuccess(produto),
        error => this.actionsForError(error)
      )
  }

  protected actionsForSuccess(produto: Produto) {
    toastr.success("Solicitação processada com sucesso!");

    const baseComponentPath: string = this.route.snapshot.parent.url[0].path;
    console.log(baseComponentPath)

    // redirect/reload component page
    this.router.navigateByUrl(baseComponentPath, { skipLocationChange: true }).then(
      () => this.router.navigate([baseComponentPath])
    )
  }

  protected actionsForError(error) {
    toastr.error("Ocorreu um erro ao processar a sua solicitação!");

    this.submittingForm = false;

    if (error.status === 422)
      this.serverErrorMessages = JSON.parse(error._body).errors;
    else
      this.serverErrorMessages = ["Falha na comunicação com o servidor. Por favor, tente mais tarde."]
  }

  protected buildResourceForm() {
    this.resourceForm = this.formBuilder.group({
      id: [null],
      nome: [null, [Validators.required, Validators.minLength(2)]],
      valor: [null, [Validators.required, Validators.min(1)]]
    });
  }

}
