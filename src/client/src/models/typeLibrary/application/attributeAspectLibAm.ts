import { Aspect } from "../enums/aspect";

export interface AttributeAspectLibAm {
  name: string;
  parentId: string;
  aspect: Aspect;
  description: string;
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
}
