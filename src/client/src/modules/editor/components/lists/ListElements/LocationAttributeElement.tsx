import { AttributeType } from "../../../../../models";
import { ListElem } from "../../../../../compLibrary/list";
import { CheckboxContainer, Label } from "../../inputs/CheckboxContainer";
import { OnPropertyChangeFunction } from "../../../types";

interface Props {
  attribute: AttributeType;
  defaultValue?: string[];
  onChange: OnPropertyChangeFunction;
}

export const AttributeElement = ({ attribute, defaultValue, onChange }: Props) => (
  <ListElem isSelected={defaultValue?.includes(attribute.id) ?? false}>
    <CheckboxContainer
      id={attribute.id}
      name={attribute.description}
      label={Label.attributeTypes}
      defaultValue={defaultValue}
      onChange={onChange}
    />
  </ListElem>
);

export default AttributeElement;
