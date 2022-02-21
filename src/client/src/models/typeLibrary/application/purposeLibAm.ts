import { Discipline } from "../enums/discipline";

export interface PurposeLibAm {
  name: string;
  discipline: Discipline;
  description: string;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
}
