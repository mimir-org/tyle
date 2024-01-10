import { PlusCircle, XCircle } from "@styled-icons/heroicons-outline";
import Input from "components/Input";
import React from "react";
import { XsdDataType } from "types/attributes/xsdDataType";
import { VALUE_LENGTH } from "types/common/stringLengthConstants";
import { AddValueListItemWrapper, ValueListFieldsWrapper, ValueListItemsWrapper } from "./ValueListFields.styled";

interface ValueListItem {
  id: string;
  value: string;
}

interface ValueListFieldsProps {
  valueList: ValueListItem[];
  setValueList: (valueList: ValueListItem[]) => void;
  dataType: XsdDataType;
}

const createEmptyValueListItem = () => ({
  id: crypto.randomUUID(),
  value: "",
});

const ValueListFields = ({ valueList, setValueList, dataType }: ValueListFieldsProps) => {
  const valueListRef = React.useRef<(HTMLInputElement | null)[]>([]);

  React.useEffect(() => {
    if (valueList.length === 0) setValueList([createEmptyValueListItem()]);
  }, [valueList, setValueList]);

  const validateDuplication = (valueListToValidate: ValueListItem[]) => {
    const values = valueListToValidate.map((item) =>
      dataType === XsdDataType.String ? item.value : item.value === "" ? NaN : Number(item.value),
    );
    const duplicateIndexes: number[] = [];

    values.forEach((item, index) => {
      if (item === "" || Number.isNaN(item)) return;

      if (values.indexOf(item) !== index) {
        if (!duplicateIndexes.includes(values.indexOf(item))) duplicateIndexes.push(values.indexOf(item));
        duplicateIndexes.push(index);
      }
    });

    for (let i = 0; i < valueList.length; i++) {
      valueListRef.current[i]?.setCustomValidity(duplicateIndexes.includes(i) ? "The value is not unique" : "");
    }
  };

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>, changeIndex: number) => {
    const nextValueList = [...valueList];
    nextValueList[changeIndex].value = event.target.value;

    validateDuplication(nextValueList);

    setValueList(nextValueList);
  };

  const handleRemove = (id: string) => {
    const nextValueList = valueList.filter((x) => x.id !== id);
    validateDuplication(nextValueList);
    setValueList(nextValueList);
  };

  const handleAdd = () => setValueList([...valueList, createEmptyValueListItem()]);

  const validationAttributes = [
    { maxLength: VALUE_LENGTH },
    { pattern: "^-?([0-9]+.)?[0-9]+$", title: "Enter a valid decimal number, e.g. -12.34" },
    { pattern: "^-?[0-9]+$", title: "Enter a valid integer, e.g. -32" },
  ];

  return (
    <ValueListFieldsWrapper>
      {valueList.map((item, index) => (
        <ValueListItemsWrapper key={item.id}>
          <Input
            required={true}
            {...validationAttributes[dataType]}
            value={item.value}
            onChange={(event) => handleChange(event, index)}
            ref={(element) => (valueListRef.current[index] = element)}
          />
          {valueList.length > 1 && <XCircle onClick={() => handleRemove(item.id)} />}
        </ValueListItemsWrapper>
      ))}
      <AddValueListItemWrapper>
        <PlusCircle onClick={handleAdd} />
      </AddValueListItemWrapper>
    </ValueListFieldsWrapper>
  );
};

export default ValueListFields;
