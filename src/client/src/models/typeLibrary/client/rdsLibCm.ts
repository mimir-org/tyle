import { Aspect } from "../enums/aspect";

export interface RdsLibCm {
  id: string;
  rdsCategoryId: string;
  name: string;
  iri: string;
  code: string;
  aspect: Aspect;
  kind: string;
}
