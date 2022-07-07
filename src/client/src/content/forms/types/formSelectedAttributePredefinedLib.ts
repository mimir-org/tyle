import { SelectedAttributePredefinedLibAm } from "@mimirorg/typelibrary-types";
import { ValueObject } from "./valueObject";

/**
 * This type functions as a layer between client needs and the backend model.
 * It allows you to adapt the expected api model to fit client/form logic needs.
 */
export interface FormSelectedAttributePredefinedLib extends Omit<SelectedAttributePredefinedLibAm, "values"> {
  values: ValueObject<string>[];
}

/**
 * Maps the client-only model back to the model expected by the backend api
 * @param formSelectedAttribute client-only model
 */
export const mapFormSelectedAttributePredefinedLibToApiModel = (
  formSelectedAttribute: FormSelectedAttributePredefinedLib
): SelectedAttributePredefinedLibAm => {
  const predefinedAttributesMap: { [key: string]: boolean } = {};
  formSelectedAttribute.values?.forEach((x) => (predefinedAttributesMap[x.value] = true));

  return {
    ...formSelectedAttribute,
    values: predefinedAttributesMap,
  };
};
