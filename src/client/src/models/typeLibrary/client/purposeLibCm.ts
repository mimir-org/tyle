import { Discipline } from "../enums/discipline";

export interface PurposeLibCm {
  id: string;
  name: string;
  iri: string;
  discipline: Discipline;
  description: string;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
  kind: string;
}
