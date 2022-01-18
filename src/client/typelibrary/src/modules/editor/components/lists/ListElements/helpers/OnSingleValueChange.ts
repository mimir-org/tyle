import { PredefinedAttribute } from "../../../../../../models";
import { ChangeEvent } from "react";

const OnSingleValueChange = (
  e: ChangeEvent<HTMLInputElement>,
  name: string,
  attributes: PredefinedAttribute[],
  isMultiSelect: boolean,
  onChange: (label: string, attributes: PredefinedAttribute[]) => void
) => {
  const targetKey = e.target.value;
  let attribute = attributes.find((a) => a.key === name);

  const valueslist = { ...attribute?.values };
  if (valueslist) valueslist[targetKey] = !valueslist[targetKey];

  const entries = Object.entries(valueslist).filter(([key, _value]) => key !== targetKey);
  entries.forEach(([key, value]) => {
    if (value) valueslist[key] = false;
    return [key, value];
  });

  attribute = {
    key: name,
    values: valueslist,
    isMultiSelect: isMultiSelect,
  };

  const updateAttributes = attributes.map((a) => {
    if (a.key === attribute.key) a = attribute;
    return a;
  });

  onChange("predefinedAttributes", updateAttributes);
};

export default OnSingleValueChange;
