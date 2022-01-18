/* eslint-disable @typescript-eslint/no-explicit-any */
import { Checkbox } from "../../../../compLibrary/input/checkbox/common";
import { AttributeName, LabelWrapper } from "./styled";

export enum Label {
  attributeTypes = 0,
  Terminals = 1,
  simpleTypes = 2,
}

interface Props {
  id: string;
  name?: string;
  label: Label;
  defaultValue?: any;
  // eslint-disable-next-line @typescript-eslint/ban-types
  onChange: Function;
}

export const CheckboxContainer = ({ id, name, label, defaultValue, onChange }: Props) => {
  const isSelected = (): boolean => {
    if (label === Label.attributeTypes || label === Label.simpleTypes) return defaultValue?.includes(id);
    if (label === Label.Terminals) return defaultValue?.some((a: { key?: string; }) => a?.key === id);
    return false;
  };

  const onCheckboxChange = () => {
    if (label === Label.attributeTypes || label === Label.simpleTypes) {
      let array = [...defaultValue];
      if (id && isSelected()) array = array.filter((a) => a !== id);
      else if (id && !isSelected() && array) array.push(id);
      onChange(Label[label], array);
    } else if (label === Label.Terminals) onChange();
  };

  return (
    <LabelWrapper>
      <Checkbox isChecked={isSelected()} onChange={() => onCheckboxChange()} />
      <AttributeName>{name}</AttributeName>
    </LabelWrapper>
  );
};

export default CheckboxContainer;
