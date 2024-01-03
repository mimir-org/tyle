import { XCircle } from "@styled-icons/heroicons-outline";
import Checkbox from "components/Checkbox";
import Input from "components/Input";
import Token from "components/Token";
import { useState } from "react";
import { AttributeTypeReferenceView } from "types/common/attributeTypeReferenceView";
import { AttributeRowWrapper, TokenWrapper } from "./AttributeRow.styled";

interface AttributeRowProps {
  value: AttributeTypeReferenceView;
  onChange: (attributeTypeReference: AttributeTypeReferenceView) => void;
  remove: () => void;
}

const AttributeRow = ({ value, onChange, remove }: AttributeRowProps) => {
  const [minCount, setMinCount] = useState(value.minCount);
  const [maxCount, setMaxCount] = useState(value.maxCount);

  const handleMinCountChange = (nextMinCount: number) => {
    let nextMaxCount = null;
    if (maxCount !== null) {
      nextMaxCount = Math.max(maxCount, nextMinCount);
    }

    setMinCount(nextMinCount);
    setMaxCount(nextMaxCount);
    onChange({ ...value, minCount: nextMinCount, maxCount: nextMaxCount });
  };

  const handleCheckedChange = (checked: boolean) => {
    if (checked) {
      const nextMaxCount = Math.max(1, value.minCount);
      setMaxCount(nextMaxCount);
      onChange({ ...value, maxCount: nextMaxCount });
    } else {
      setMaxCount(null);
      onChange({ ...value, maxCount: null });
    }
  };

  const handleMaxCountChange = (nextMaxCount: number) => {
    setMaxCount(nextMaxCount);
    onChange({ ...value, maxCount: nextMaxCount });
  };

  return (
    <AttributeRowWrapper>
      <TokenWrapper>
        <Token
          variant={"secondary"}
          actionable
          actionIcon={<XCircle />}
          actionText="Remove attribute"
          onAction={remove}
          dangerousAction
        >
          {value.attribute.name}
        </Token>
      </TokenWrapper>
      <Input
        type="number"
        min={0}
        value={minCount}
        onChange={(event) => handleMinCountChange(Number(event.target.value))}
      />
      <Checkbox checked={maxCount !== null} onCheckedChange={(checked) => handleCheckedChange(!!checked)} />
      <Input
        type="number"
        min={Math.max(minCount, 1)}
        disabled={maxCount === null}
        value={maxCount ?? 0}
        onChange={(event) => handleMaxCountChange(Number(event.target.value))}
      />
    </AttributeRowWrapper>
  );
};

export default AttributeRow;
