import {Component} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-trucks-editor-modal',
  templateUrl: './trucks-editor-modal.component.html',
  styleUrls: ['./trucks-editor-modal.component.scss']
})
export class TrucksEditorModalComponent {
  public form!: FormGroup;

  public get canSave(): boolean {
    return this.form.valid && this.form.dirty;
  }

  constructor(private formBuilder: FormBuilder, private matDialogRef: MatDialogRef<TrucksEditorModalComponent>) {
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
      licensePlate: [null, [Validators.required]],
      model: [null, [Validators.required]],
      manufacturingYear: [null, [Validators.required]],
    });
  }
}
