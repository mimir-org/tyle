import { Input } from "@mimirorg/component-library";
import { PlusCircle, XCircle } from "@styled-icons/heroicons-outline";
import React from "react";
import { VALUE_LENGTH } from "types/common/stringLengthConstants";
import { AddValueListItemWrapper, ValueListFieldsWrapper, ValueListItemsWrapper } from "./ValueListFields.styled";

interface ValueListItem {
  id: string;
  value: string;
}

interface ValueListFieldsProps {
  valueList: ValueListItem[];
  setValueList: (valueList: ValueListItem[]) => void;
  valueListRef: React.MutableRefObject<(HTMLInputElement | null)[]>;
}

const createEmptyValueListItem = () => ({
  id: crypto.randomUUID(),
  value: "",
});

export const StringValueListFields = ({ valueList, setValueList, valueListRef }: ValueListFieldsProps) => {
  React.useEffect(() => {
    if (valueList.length === 0) setValueList([createEmptyValueListItem()]);
  }, [valueList, setValueList]);

  const validateDuplication = (valueListToValidate: ValueListItem[]) => {
    const values = valueListToValidate.map((item) => item.value);
    const duplicateIndexes: number[] = [];

    values.forEach((item, index) => {
      if (values.indexOf(item) !== index) {
        if (!duplicateIndexes.includes(values.indexOf(item))) duplicateIndexes.push(values.indexOf(item));
        duplicateIndexes.push(index);
      }
    });

    for (let i = 0; i < valueList.length; i++) {
      valueListRef.current[i]?.setCustomValidity(duplicateIndexes.includes(i) ? "Value is not unique" : "");
    }
  };

  const handleChange = (event: React.ChangeEvent<HTMLInputElement>, changeIndex: number) => {
    const nextValueList = [...valueList];
    nextValueList[changeIndex].value = event.target.value;

    validateDuplication(nextValueList);

    setValueList(nextValueList);
  };

  return (
    <ValueListFieldsWrapper>
      {valueList.map((item, index) => (
        <ValueListItemsWrapper key={item.id}>
          <Input
            required={true}
            maxLength={VALUE_LENGTH}
            value={item.value}
            onChange={(event) => handleChange(event, index)}
            ref={(element) => (valueListRef.current[index] = element)}
          />
          {valueList.length > 1 && (
            <XCircle
              onClick={() => {
                const nextValueList = valueList.filter((x) => x.id !== item.id);
                validateDuplication(nextValueList);
                setValueList(nextValueList);
              }}
            />
          )}
        </ValueListItemsWrapper>
      ))}
      <AddValueListItemWrapper>
        <PlusCircle onClick={() => setValueList([...valueList, { id: crypto.randomUUID(), value: "" }])} />
      </AddValueListItemWrapper>
    </ValueListFieldsWrapper>
  );
};

export const DecimalValueListFields = ({ valueList, setValueList }: ValueListFieldsProps) => {
  return <></>;
};

export const IntegerValueListFields = ({ valueList, setValueList }: ValueListFieldsProps) => {
  return <></>;
};
