import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AClass } from 'src/app/shared/models/Education/AClass';
import { JClassSection } from 'src/app/shared/models/Education/JClassSection';
import { OLinkClassSection } from 'src/app/shared/models/Education/OLinkClassSection';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-o-link-class-section',
  templateUrl: './o-link-class-section.component.html',
  styleUrls: ['./o-link-class-section.component.css']
})


export class OLinkClassSectionComponent implements OnInit {
  linkClassSections: OLinkClassSection[] = [];
  linkClassSectionForm!: FormGroup;
  classes: AClass[] = [];
  classSections: JClassSection[] = [];
  isLoadingResults = true;
  isRateLimitReached = false;
  sectionsArray!: FormArray;
  private linkClassSectionSubscription: Subscription | undefined;

  constructor(
    private fb: FormBuilder,
    private services: EducationService,
    private snackbar: MatSnackBar
  ) {
    this.initForm();
  }

  ngOnInit() {
    this.onLoad();
    // Fetch classes first
    this.services.getClasses().subscribe(
      (response: AClass[]) => {
        this.classes = [];
        this.classes = response;  
        // After fetching classes, fetch class sections
        this.services.getClassSections().subscribe(
          (response: JClassSection[]) => {
            this.classSections = [];
            this.classSections = response; 
          }
        );
      }
    );
  
    // You can place other code that doesn't depend on the data here
  }

  ngOnDestroy(){
    if(this.linkClassSectionSubscription) this.linkClassSectionSubscription.unsubscribe();
  }

  onLoad() {
    this.linkClassSectionSubscription = this.services.getLinkClassSections().subscribe({
      next: (response: OLinkClassSection[]) => {
        this.linkClassSections = [];
        this.linkClassSections = response;
        this.isLoadingResults = false;
        this.initForm();
      },
      error: (error) => {
        console.error(error);
        this.isLoadingResults = true;
        this.isRateLimitReached = false;
      }
    });
  }

  convertToFormControl(absCtrl: AbstractControl | null): FormControl {
    return absCtrl as FormControl;
  }

  initForm() {
    this.linkClassSectionForm = this.fb.group({
      linkClassSectionId: [null],
      classId: [null, Validators.required],
      sections: this.fb.array([]) // Initialize sections as an empty FormArray
    });

    this.sectionsArray = this.linkClassSectionForm.get('sections') as FormArray;

    // Add a value change listener to the class dropdown
    this.linkClassSectionForm.get('classId')?.valueChanges.subscribe((classId) => {
      this.updateCheckboxOptions(Number(classId));
    });
  }

  resetForm() {
    // Clear the checked checkboxes
    this.sectionsArray.controls.forEach((control) => {
      control.get('isChecked')?.setValue(false);
    });
  }

  toggleCheckbox(index: number) {
    const isCheckedControl = this.sectionsArray.controls[index].get('isChecked');
    if (isCheckedControl) {
      isCheckedControl.setValue(!isCheckedControl.value);
    }
  }
  
  updateCheckboxOptions(classId: number | null): void {
    debugger;
    const selectedSections = this.linkClassSections.filter(x => x.classId === Number(classId)).map(section => section.classSectionId);

    const SectionsWithCheckboxes = this.classSections.map(section => {
      return {
        ...section,
        // isChecked: classId ? selectedSections.includes(section.classSectionId) : false
        isChecked: classId ? this.isSectionChecked(section, classId) : false
      };
    });
  
    // Clear and reinitialize the sectionsArray
    this.sectionsArray.clear();
  
    for (const section of SectionsWithCheckboxes) {
      const sectionSection = this.fb.group({
        isChecked: new FormControl(section.isChecked),
        classSectionName: new FormControl(section.classSectionName),
        classSectionId: new FormControl(section.classSectionId)
      });
      this.sectionsArray.push(sectionSection);
    }
  }
  
  private isSectionChecked(section: JClassSection, classId: number): boolean {
    // Check if the section is associated with the selected class
    return this.linkClassSections.some(
      x => x.classId === classId && x.classSectionId === section.classSectionId
    );
  }

  onSubmit() {
    debugger;
    if (this.linkClassSectionForm.valid) {
      const formData = this.linkClassSectionForm.value;
      const selectedSections = formData.sections.filter((section: any) => section.isChecked);
      
      let newRecordArray!: OLinkClassSection[];
      if(selectedSections.length === 0)
      {
        newRecordArray = [
          {
            linkClassSectionId: 0,
            classId: formData.classId,
            className: formData.className,
            classSectionId: formData.classGroupId
          }
        ];
      }
      else{
         newRecordArray = selectedSections.map((subject: any) => ({
          linkClassSubjectId: 0,
          classId: formData.classId,
          className: formData.className,
          classSectionId: formData.classGroupId,
          classSectionName: subject.classSubjectId
        }));
      }

      this.services.insertLinkClassSection(newRecordArray).subscribe(() => {
        this.resetForm();
        this.onLoad();
        this.snackbar.open('New record(s) inserted successfully!', 'Dismiss', {
          duration: 1500,
        });
      },
        (error) => {
          console.error('Error inserting data:', error);
        }
      );
    } else {
      this.snackbar.open('Please fill the required fields!', 'Dismiss', {
        duration: 2000,
      });
    }
  }
}