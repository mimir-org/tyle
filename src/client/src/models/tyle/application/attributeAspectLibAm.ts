import { Aspect } from "../enums/aspect";

export interface AttributeAspectLibAm {
  name: string;
  aspect: Aspect;
  contentReferences: string[];
  parentId: string;
  description: string;
}
