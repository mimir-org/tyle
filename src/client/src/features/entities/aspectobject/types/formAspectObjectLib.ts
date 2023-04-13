import { Aspect, AspectObjectLibAm, AspectObjectLibCm } from "@mimirorg/typelibrary-types";
import {
  FormAttributePredefinedLib,
  mapAttributePredefinedLibCmToClientModel,
  mapFormAttributePredefinedLibToApiModel,
} from "features/entities/aspectobject/types/formAttributePredefinedLib";
import {
  FormAspectObjectTerminalLib,
  mapAspectObjectTerminalLibCmToClientModel,
} from "features/entities/aspectobject/types/formAspectObjectTerminalLib";
import { AspectObjectFormMode } from "features/entities/aspectobject/types/aspectObjectFormMode";
import { ValueObject } from "features/entities/types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormAspectObjectLib
  extends Omit<AspectObjectLibAm, "attributes" | "selectedAttributePredefined" | "aspectObjectTerminals"> {
  attributes: ValueObject<string>[];
  selectedAttributePredefined: FormAttributePredefinedLib[];
  aspectObjectTerminals: FormAspectObjectTerminalLib[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formAspectObject client-only model
 */
export const mapFormAspectObjectLibToApiModel = (formAspectObject: FormAspectObjectLib): AspectObjectLibAm => ({
  ...formAspectObject,
  attributes: formAspectObject.attributes.map((x) => x.value),
  selectedAttributePredefined: formAspectObject.selectedAttributePredefined.map((x) =>
    mapFormAttributePredefinedLibToApiModel(x)
  ),
});

export const mapAspectObjectLibCmToClientModel = (
  aspectObject: AspectObjectLibCm,
  mode?: AspectObjectFormMode
): FormAspectObjectLib => ({
  ...aspectObject,
  parentId: mode === "clone" ? aspectObject.id : aspectObject.parentId,
  attributes: aspectObject.attributes.map((x) => ({ value: x.id })),
  aspectObjectTerminals: aspectObject.aspectObjectTerminals.map(mapAspectObjectTerminalLibCmToClientModel),
  selectedAttributePredefined: aspectObject.selectedAttributePredefined.map(mapAttributePredefinedLibCmToClientModel),
});

export const createEmptyFormAspectObjectLib = (): FormAspectObjectLib => ({
  ...emptyAspectObjectLibAm,
  attributes: [],
  selectedAttributePredefined: [],
  aspectObjectTerminals: [],
});

const emptyAspectObjectLibAm: AspectObjectLibAm = {
  name: "",
  rdsName: "",
  rdsCode: "",
  purposeName: "",
  aspect: Aspect.None,
  companyId: 0,
  attributes: [],
  aspectObjectTerminals: [],
  selectedAttributePredefined: [],
  description: "",
  symbol: "",
  typeReference: "",
  parentId: "",
  version: "1.0",
};
