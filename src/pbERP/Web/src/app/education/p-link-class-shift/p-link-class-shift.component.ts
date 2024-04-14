import { Component } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AClass } from 'src/app/shared/models/Education/AClass';
import { LClassShift } from 'src/app/shared/models/Education/LClassShift';
import { PLinkClassShift } from 'src/app/shared/models/Education/PLinkClassShift';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NLinkClassGroup } from 'src/app/shared/models/Education/NLinkClassGroup';

@Component({
  selector: 'app-p-link-class-shift',
  templateUrl: './p-link-class-shift.component.html',
  styleUrls: ['./p-link-class-shift.component.css']
})
export class PLinkClassShiftComponent {
  linkClassShiftForm!: FormGroup;
  linkClassShifts: PLinkClassShift[] = [];
  classes: AClass[] = [];
  classShifts: LClassShift[] = [];
  isLoadingResults = true;
  isRateLimitReached = false;
  shiftsArray!: FormArray;

  private linkClassShiftSubscription: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private services: EducationService,
    private snackbar: MatSnackBar
  ) {
    this.intForm();
  }

  ngOnInit() {
    this.onLoad();
    this.services.getClasses().subscribe(
      (response: AClass[]) => {
        this.classes = [];
        this.classes = response;

        this.services.getClassShifts().subscribe(
          (response: LClassShift[]) => {
            this.classShifts = [];
            this.classShifts = response;
          }
        );
      }
    );
  }

  ngOnDestroy(){
    if(this.linkClassShiftSubscription) this.linkClassShiftSubscription.unsubscribe();
  }

  intForm() {
    this.linkClassShiftForm = this.fb.group({
      linkClassShiftId: [null],
      classId: [null, Validators.required],
      shifts: this.fb.array([])
    });

    this.shiftsArray = this.linkClassShiftForm.get('shifts') as FormArray;

    this.linkClassShiftForm.get('classId')?.valueChanges.subscribe((classId) => {
      this.updateCheckboxOptions(Number(classId));
    });
  }

  onLoad() {
    this.linkClassShiftSubscription = this.services.getLinkClassShifts().subscribe({
      next: (response: PLinkClassShift[]) => {
        this.linkClassShifts = [];
        this.linkClassShifts = response;
        this.isLoadingResults = false;
        this.intForm();
      },
      error: (error) => {
        console.error(error);
        this.isLoadingResults = true;
        this.isRateLimitReached = false;
      }
    });
  }

  updateCheckboxOptions(classId: number | null): void {
    const selectedShift = this.linkClassShifts.filter(x => x.classId == Number(classId)).map(x => x.classShiftId);

    const shiftsWtihCheckboxes = this.classShifts.map(shift => {
      return {
        ...shift,
        isChecked: classId ? this.isShiftChecked(shift, classId) : false
      };
    });

    this.shiftsArray.clear();

    for (const shift of shiftsWtihCheckboxes) {
      const shiftGroup = this.fb.group({
        isChecked: new FormControl(shift.isChecked),
        classShiftName: new FormControl(shift.classShiftName),
        classShiftId: new FormControl(shift.classShiftId)
      });
      this.shiftsArray.push(shiftGroup);
    }
  }

  private isShiftChecked(shift: LClassShift, classId: number): boolean {
    return this.linkClassShifts.some(
      x => x.classId == classId && x.classShiftId === shift.classShiftId
    );
  }

  toggleCheckBox(index: number){
    const isCheckedControl = this.shiftsArray.controls[index].get('isChecked');
    if(isCheckedControl){
      isCheckedControl.setValue(!isCheckedControl.value);
    }
  }

  convertToFormControl(absCtrl: AbstractControl | null): FormControl {
    return absCtrl as FormControl;
  }
  
  resetForm() {
    this.shiftsArray.controls.forEach((control) => {
      control.get('isChecked')?.setValue(false);
    });
  }

  onSubmit() {
    if (this.linkClassShiftForm.valid) {
      const formData = this.linkClassShiftForm.value;
      const selectedShifts = formData.shifts.filter((shift: any) => shift.isChecked);

      let newRecordArray: PLinkClassShift[];
      if(selectedShifts.length === 0)
      {
        newRecordArray = [
          {
            linkClassShiftId: 0,
            classId : formData.classId,
            className : formData.className,
            classShiftId : formData.classShiftId
          }
        ];
      }
      else{
        newRecordArray = selectedShifts.map((shift: any) => ({
          linkClassShiftId: 0,
          classId: formData.classId,
          ClassName: formData.ClassName,
          classShiftId: shift.classShiftId,
          classShiftName: shift.classShiftName
        }));
      } 

      this.services.insertLinkClassShift(newRecordArray).subscribe(() => {
        this.resetForm();
        this.onLoad();
        this.snackbar.open('New record(s) inserted successfully!', 'Dismiss', {duration : 1500});
      });
    } else {
      this.snackbar.open('Please fill the required fields', 'Dismiss', { duration: 2000 });
    }
  }




}
