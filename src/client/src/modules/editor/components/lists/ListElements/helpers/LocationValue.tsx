import { ValuesListItem, ValuesListWrapper } from "../../../../styled";
import { Checkbox } from "../../../../../../compLibrary/input/checkbox/common";
import { RadioButton } from "../../../../../../compLibrary/input/radiobutton";
import { AttributeName } from "../../../inputs/styled";
import { OnMultipleValuesChange, OnSingleValueChange } from "./index";
import { PredefinedAttribute } from "../../../../../../models";
import { OnPropertyChangeFunction } from "../../../../types";

interface Props {
  isMultiSelect: boolean;
  getValues: () => Record<string, boolean>;
  attributeName: string;
  defaultValue: PredefinedAttribute[];
  onChange: OnPropertyChangeFunction;
}

const LocationValue = ({ isMultiSelect, getValues, attributeName, defaultValue, onChange }: Props) => {
  const onMultipleValuesChange = ([param_key, param_value]) => {
    OnMultipleValuesChange([param_key, param_value], attributeName, defaultValue, isMultiSelect, onChange);
  };

  return (
    <ValuesListWrapper>
      {Object.entries(getValues()).map(([key, value]) => {
        return isMultiSelect ? (
          <ValuesListItem key={key}>
            <Checkbox isChecked={value} onChange={() => onMultipleValuesChange([key, value])} id={key} />
            <AttributeName>{key}</AttributeName>
          </ValuesListItem>
        ) : (
          <ValuesListItem key={key} gap={"25px"}>
            <RadioButton
              isChecked={value}
              onChange={(e) => OnSingleValueChange(e, attributeName, defaultValue, isMultiSelect, onChange)}
              id={key}
            />
            <AttributeName>{key}</AttributeName>
          </ValuesListItem>
        );
      })}
    </ValuesListWrapper>
  );
};

export default LocationValue;
