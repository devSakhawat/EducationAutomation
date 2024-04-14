import { Component } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AClass } from 'src/app/shared/models/Education/AClass';
import { QLinkClassSubject } from 'src/app/shared/models/Education/QLinkClassSubject';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { NLinkClassGroup } from 'src/app/shared/models/Education/NLinkClassGroup';
import { MClassSubject } from 'src/app/shared/models/Education/MClassSubject';

@Component({
  selector: 'app-q-link-class-subject',
  templateUrl: './q-link-class-subject.component.html',
  styleUrls: ['./q-link-class-subject.component.css']
})


export class QLinkClassSubjectComponent {
  linkClassSubjectForm!: FormGroup;
  linkClassSubjects: QLinkClassSubject[] = [];
  classes: AClass[] = [];
  linkClassGroups: NLinkClassGroup[] = [];
  selectedLinkClassGroups: NLinkClassGroup[] = [];  
  classSubjects: MClassSubject[] = [];
  isLoadingResults = true;
  isRateLimitReached = false;
  subjectsArray!: FormArray;

  private linkClassSubjectSubscription: Subscription | undefined;

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

        this.services.getLinkClassGroups().subscribe(
          (response: NLinkClassGroup[]) => {
            this.linkClassGroups = [];
            this.linkClassGroups = response;
            console.log(this.linkClassGroups);
          }
        );

        this.services.getClassSubjects().subscribe(
          (response: MClassSubject[]) => {
            this.classSubjects = [];
            this.classSubjects = response;
            console.log(this.classSubjects);
          }
        );
      }
    );
  }

  ngOnDestroy(){
    if(this.linkClassSubjectSubscription) this.linkClassSubjectSubscription.unsubscribe();
  }

  intForm() {
    this.linkClassSubjectForm = this.fb.group({
      linkClassSubjectId: [null],
      classId: [null, Validators.required],
      classGroupId: [null, Validators.required],
      classSubjectId: [0, Validators.required],
      subjects: this.fb.array([])
    });

    this.subjectsArray = this.linkClassSubjectForm.get('subjects') as FormArray;

    this.linkClassSubjectForm.get('classId')?.valueChanges.subscribe((classId) => {
      this.updateClassGroupDropdown(Number(classId));
    })

    this.linkClassSubjectForm.get('classGroupId')?.valueChanges.subscribe((classGroupId) => {
      this.updateCheckboxOptions(Number(classGroupId));
    });
  }

  onLoad() {
    this.linkClassSubjectSubscription = this.services.getLinkClassSubjects().subscribe({
      next: (response: QLinkClassSubject[]) => {
        this.linkClassSubjects = [];
        this.linkClassSubjects = response;
        this.isLoadingResults = false;
        this.intForm();
        console.log(this.linkClassSubjects);
      },
      error: (error) => {
        console.error(error);
        this.isLoadingResults = true;
        this.isRateLimitReached = false;
      }
    });
  }

  updateClassGroupDropdown(classId: number | null): void {
    this.selectedLinkClassGroups = this.linkClassGroups.filter(x => x.classId == Number(classId));
  }

  toggleCheckBox(index: number){
    const isCheckedControl = this.subjectsArray.controls[index].get('isChecked');
    if(isCheckedControl){
      isCheckedControl.setValue(!isCheckedControl.value);
    }
  }


  updateCheckboxOptions(classGroupId: number | null): void {
    // const selectedGroups = this.linkClassSubjects.filter(x => x.classGroupId === Number(classGroupId)).map(group => group.classGroupId);
    const SubjectsWithCheckboxes = this.classSubjects.map(subject => {
      return {
        ...subject,
        isChecked: classGroupId ? this.isSubjectChecked(subject, classGroupId) : false
      };
    });
    this.subjectsArray.clear();
  
    for (const subject of SubjectsWithCheckboxes) {
      const subjectWithCheckbox = this.fb.group({
        isChecked: new FormControl(subject.isChecked),
        classSubjectName: new FormControl(subject.classSubjectName),
        classSubjectId: new FormControl(subject.classSubjectId)
      });
      this.subjectsArray.push(subjectWithCheckbox);
    }
  }

  private isSubjectChecked(subject: MClassSubject, classGroupId: number): boolean {
    return this.linkClassSubjects.some(
      x => x.classGroupId === classGroupId && x.classSubjectId ===subject.classSubjectId
    );
  }


  convertToFormControl(absCtrl: AbstractControl | null): FormControl {
    return absCtrl as FormControl;
  }
  
  resetForm() {
    this.selectedLinkClassGroups = [];
    this.subjectsArray.controls.forEach((control) => {
      control.get('isChecked')?.setValue(false);
    });
  }

  onSubmit() {
    console.log(this.linkClassSubjectForm);
    debugger;
    if (this.linkClassSubjectForm.valid) {
      const formData = this.linkClassSubjectForm.value;
      const selectedSubjects = formData.subjects.filter((subject: any) => subject.isChecked);
      let newRecordArray!: QLinkClassSubject[];
      if(selectedSubjects.length === 0)
      {
        newRecordArray = [
          {linkClassSubjectId: 0,
            classId: formData.classId,
            className: formData.className,
            classGroupId: formData.classGroupId,
            classGroupName: formData.classGroupName,
            classSubjectId: formData.classSubjectId
          }
        ];
      }
      else{
         newRecordArray = selectedSubjects.map((subject: any) => ({
          linkClassSubjectId: 0,
          classId: formData.classId,
          className: formData.className,
          classGroupId: formData.classGroupId,
          classGroupName: formData.classGroupName,
          classSubjectId: subject.classSubjectId,
          classSubjectName: subject.classSubjectName
        }));
      }

      this.services.insertLinkClassSubject(newRecordArray).subscribe(() => {
        this.resetForm();
        this.onLoad();
        this.snackbar.open('New record(s) inserted successfully!', 'Dismiss', {duration : 1500});
      });
    } else {
      this.snackbar.open('Please fill the required fields', 'Dismiss', { duration: 2000 });
    }
  }
}

