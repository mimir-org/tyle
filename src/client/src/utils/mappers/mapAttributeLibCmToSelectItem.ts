import { AttributeLibCm } from "@mimirorg/typelibrary-types";
import { SelectItem } from "../../content/types/SelectItem";

export const mapAttributeLibCmToSelectItem = (attribute: AttributeLibCm): SelectItem => ({
  id: attribute.id,
  name: attribute.name,
  traits: {
    condition: attribute.attributeCondition,
    qualifier: attribute.attributeQualifier,
    source: attribute.attributeSource,
  },
});
