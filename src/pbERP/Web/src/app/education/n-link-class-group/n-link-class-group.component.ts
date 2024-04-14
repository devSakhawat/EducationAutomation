import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AClass } from 'src/app/shared/models/Education/AClass';
import { NLinkClassGroup } from 'src/app/shared/models/Education/NLinkClassGroup';
import { EducationService } from '../education.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { KClassGroup } from 'src/app/shared/models/Education/KClassGroup';

@Component({
  selector: 'app-n-link-class-group',
  templateUrl: './n-link-class-group.component.html',
  styleUrls: ['./n-link-class-group.component.css']
})

export class NLinkClassGroupComponent implements OnInit {
  linkClassGroups: NLinkClassGroup[] = [];
  linkClassGroupForm!: FormGroup;
  classes: AClass[] = [];
  classGroups: KClassGroup[] = [];
  isLoadingResults = true;
  isRateLimitReached = false;
  groupsArray!: FormArray;
  private linkClassGroupSubscription: Subscription | undefined;

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
        // After fetching classes, fetch class groups
        this.services.getClassGroups().subscribe(
          (response: KClassGroup[]) => {
            this.classGroups = [];
            this.classGroups = response;
          }
        );
      }
    );

    // You can place other code that doesn't depend on the data here
  }

  ngOnDestroy() {
    if (this.linkClassGroupSubscription) this.linkClassGroupSubscription.unsubscribe();
  }

  onLoad() {
    this.linkClassGroupSubscription = this.services.getLinkClassGroups().subscribe({
      next: (response: NLinkClassGroup[]) => {
        this.linkClassGroups = [];
        this.linkClassGroups = response;
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
    this.linkClassGroupForm = this.fb.group({
      linkClassGroupId: [null],
      classId: [null, Validators.required],
      groups: this.fb.array([]) // Initialize groups as an empty FormArray
    });

    this.groupsArray = this.linkClassGroupForm.get('groups') as FormArray;

    // Add a value change listener to the class dropdown
    this.linkClassGroupForm.get('classId')?.valueChanges.subscribe((classId) => {
      this.updateCheckboxOptions(Number(classId));
    });
  }

  resetForm() {
    // Clear the checked checkboxes
    this.groupsArray.controls.forEach((control) => {
      control.get('isChecked')?.setValue(false);
    });
  }

  toggleCheckbox(index: number) {
    const isCheckedControl = this.groupsArray.controls[index].get('isChecked');
    if (isCheckedControl) {
      isCheckedControl.setValue(!isCheckedControl.value);
    }
  }




  updateCheckboxOptions(classId: number | null): void {
    debugger;
    const selectedGroups = this.linkClassGroups
      .filter(x => x.classId === Number(classId))
      .map(group => group.classGroupId);

    const GroupsWithCheckboxes = this.classGroups.map(group => {
      return {
        ...group,
        // isChecked: classId ? selectedGroups.includes(group.classGroupId) : false
        isChecked: classId ? this.isGroupChecked(group, classId) : false
      };
    });

    // Clear and reinitialize the groupsArray
    this.groupsArray.clear();

    for (const group of GroupsWithCheckboxes) {
      const groupGroup = this.fb.group({
        isChecked: new FormControl(group.isChecked),
        classGroupName: new FormControl(group.classGroupName),
        classGroupId: new FormControl(group.classGroupId)
      });
      this.groupsArray.push(groupGroup);
    }
  }

  private isGroupChecked(group: KClassGroup, classId: number): boolean {
    // Check if the group is associated with the selected class
    return this.linkClassGroups.some(
      x => x.classId === classId && x.classGroupId === group.classGroupId
    );
  }

  onSubmit() {
    debugger;
    if (this.linkClassGroupForm.valid) {
      const formData = this.linkClassGroupForm.value;
      const selectedGroups = formData.groups.filter((group: any) => group.isChecked);

      let newRecordArray!: NLinkClassGroup[];
      if (selectedGroups.length === 0) {
        newRecordArray = [
          {
            linkClassGroupId: 0,
            classId: formData.classId,
            className: formData.className,
            classGroupId: formData.classGroupId
          }
        ];
      }
      else {
        newRecordArray = selectedGroups.map((group: any) => ({
          linkClassGroupId: 0,
          classId: formData.classId,
          className: formData.className,
          classGroupId: group.classGroupId,
          classGroupName: group.classGroupName
        }));
      }


      this.services.insertLinkClassGroup(newRecordArray).subscribe(() => {
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