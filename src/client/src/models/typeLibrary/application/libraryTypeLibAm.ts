import { Aspect } from "../enums/aspect";
import { ObjectType } from "../enums/objectType";
import { TerminalItemLibAm } from "./terminalItemLibAm";
import { AttributePredefinedLibAm } from "./attributePredefinedLibAm";
import { CollectionLibAm } from "./collectionLibAm";

export interface LibraryTypeLibAm {
  parentId: string;
  name: string;
  version: string;
  firstVersionId: string;
  aspect: Aspect;
  description: string;
  rdsId: string;
  purposeId: string;
  blobId: string;
  attributeAspectId: string;
  terminalId: string;
  objectType: ObjectType;
  terminals: TerminalItemLibAm[];
  attributeIdList: string[];
  attributesPredefined: AttributePredefinedLibAm[];
  simple: string[];
  collections: CollectionLibAm[];
  updatedBy: string;
  updated: string | null;
  created: string;
  createdBy: string;
}
