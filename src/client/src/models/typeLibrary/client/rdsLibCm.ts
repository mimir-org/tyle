import { Aspect } from "../enums/aspect";
import { RdsCategoryLibCm } from "./rdsCategoryLibCm";

export interface RdsLibCm {
  id: string;
  rdsCategoryId: string;
  rdsCategory: RdsCategoryLibCm;
  name: string;
  iri: string;
  code: string;
  aspect: Aspect;
  kind: string;
}
