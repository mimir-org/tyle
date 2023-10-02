import { Aspect, BlockLibAm, BlockLibCm } from "@mimirorg/typelibrary-types";
import {
  FormAttributePredefinedLib,
  mapAttributePredefinedLibCmToClientModel,
  mapFormAttributePredefinedLibToApiModel,
} from "features/entities/block/types/formAttributePredefinedLib";
import {
  FormBlockTerminalLib,
  mapBlockTerminalLibCmToClientModel,
} from "features/entities/block/types/formBlockTerminalLib";
import { ValueObject } from "features/entities/types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormBlockLib
  extends Omit<
    BlockLibAm,
    "attributes" | "test" | "selectedAttributePredefined" | "blockTerminals" | "attributeGroups"
  > {
  attributes: ValueObject<string>[];
  selectedAttributePredefined: FormAttributePredefinedLib[];
  blockTerminals: FormBlockTerminalLib[];
  attributeGroups: ValueObject<string>[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formBlock client-only model
 */
export const mapFormBlockLibToApiModel = (formBlock: FormBlockLib): BlockLibAm => ({
  ...formBlock,
  attributes: formBlock.attributes.map((x) => x.value),
  attributeGroups: formBlock.attributeGroups.map((x) => x.value),
  selectedAttributePredefined: formBlock.selectedAttributePredefined.map((x) =>
    mapFormAttributePredefinedLibToApiModel(x),
  ),
});

export const mapBlockLibCmToClientModel = (block: BlockLibCm, newCompanyId?: number): FormBlockLib => ({
  ...block,
  companyId: newCompanyId ?? block.companyId,
  attributes: block.attributes.map((x) => ({ value: x.id })),
  blockTerminals: block.blockTerminals.map(mapBlockTerminalLibCmToClientModel),
  selectedAttributePredefined: block.selectedAttributePredefined.map(mapAttributePredefinedLibCmToClientModel),
  attributeGroups: [],
});

export const createEmptyFormBlockLib = (): FormBlockLib => ({
  ...emptyBlockLibAm,
  attributes: [],
  selectedAttributePredefined: [],
  blockTerminals: [],
  attributeGroups: [],
});

const emptyBlockLibAm: BlockLibAm = {
  name: "",
  rdsId: "",
  purposeName: "",
  aspect: Aspect.None,
  companyId: 0,
  attributes: [],
  blockTerminals: [],
  selectedAttributePredefined: [],
  description: "",
  symbol: "",
  typeReference: "",
  version: "1.0",
  attributeGroups: [],
};
