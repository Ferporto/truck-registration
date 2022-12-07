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
    this.matDialogRef.close();
  }

  private createForm(): void {
    const currentYear = Number(new Date().getFullYear());
    const nextYear = currentYear + 1;

    this.form = this.formBuilder.group({
      name: [null, [Validators.required]],
      type: [null, [Validators.required]],
      year: [null, [Validators.required, Validators.min(currentYear), Validators.max(nextYear)]],
    });
  }
}
