import {Component} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-trucks-models-editor-modal',
  templateUrl: './trucks-models-editor-modal.component.html',
  styleUrls: ['./trucks-models-editor-modal.component.scss']
})
export class TrucksModelsEditorModalComponent {
  public form!: FormGroup;

  public get canSave(): boolean {
    return this.form.valid && this.form.dirty;
  }

  constructor(private formBuilder: FormBuilder, private matDialogRef: MatDialogRef<TrucksModelsEditorModalComponent>) {
    this.createForm();
  }

  public closeModal(): void {
    this.matDialogRef.close();
  }

  public save(): void {
    // this.produtoService.adicionarProduto(this.form.getRawValue());
    this.form.reset();
  }

  private createForm(): void {
    this.form = this.formBuilder.group({
      name: [null, [Validators.required]],
      type: [null, [Validators.required]],
      year: [null, [Validators.required]],
    });
  }
}
