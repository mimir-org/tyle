import { SelectedAttributePredefinedLibAm, SelectedAttributePredefinedLibCm } from "@mimirorg/typelibrary-types";
import { ValueObject } from "features/entities/types/valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormAttributePredefinedLib extends Omit<SelectedAttributePredefinedLibAm, "values"> {
  values: ValueObject<string>[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formSelectedAttribute client-only model
 */
export const mapFormAttributePredefinedLibToApiModel = (
  formSelectedAttribute: FormAttributePredefinedLib
): SelectedAttributePredefinedLibAm => {
  const predefinedAttributesMap: { [key: string]: boolean } = {};
  formSelectedAttribute.values?.forEach((x) => (predefinedAttributesMap[x.value] = true));

  return {
    ...formSelectedAttribute,
    values: predefinedAttributesMap,
  };
};

export const mapAttributePredefinedLibCmToClientModel = (
  attribute: SelectedAttributePredefinedLibCm
): FormAttributePredefinedLib => ({
  ...attribute,
  values: Object.keys(attribute.values).map((y) => ({ value: y })),
});
