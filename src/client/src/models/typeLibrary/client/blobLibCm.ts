import { Discipline } from "../enums/discipline";

export interface BlobLibCm {
  id: string;
  name: string;
  iri: string;
  discipline: Discipline;
  data: string;
  kind: string;
}
