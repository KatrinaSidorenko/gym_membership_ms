export interface SportClass {
  id: number;
  name: string;
  date: Date;
  instructorName: string;
  price: number;
}

export interface ExtendedSportClass extends SportClass {
  paidEnrollments: number;
}

export type SportClasses = SportClass[] | ExtendedSportClass[];
