import { Box, Checkbox, Flexbox, Input, Token } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useState } from "react";
import { AttributeTypeReferenceView } from "types/common/attributeTypeReferenceView";

interface AttributeRowProps {
  field: AttributeTypeReferenceView;
  remove: () => void;
  value: AttributeTypeReferenceView;
  onChange: (attributeTypeReference: AttributeTypeReferenceView) => void;
}

const AttributeRow = ({ field, remove, value, onChange }: AttributeRowProps) => {
  const [minCount, setMinCount] = useState(field.minCount);
  const [maxCount, setMaxCount] = useState<number | null>(field.maxCount ?? null);

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
    <Flexbox alignItems={"center"}>
      <Box flex={1}>
        <Token
          variant={"secondary"}
          actionable
          actionIcon={<XCircle />}
          actionText={"Remove attribute"}
          onAction={remove}
          dangerousAction
        >
          {field.attribute.name}
        </Token>
      </Box>
      <Box>
        <Input
          type="number"
          min={0}
          value={minCount}
          onChange={(event) => handleMinCountChange(Number(event.target.value))}
        />
      </Box>
      <Box>
        <Checkbox checked={maxCount !== null} onCheckedChange={(checked) => handleCheckedChange(!!checked)} />
      </Box>
      <Box>
        <Input
          type="number"
          min={Math.max(minCount, 1)}
          disabled={maxCount === null}
          value={maxCount ?? 0}
          onChange={(event) => handleMaxCountChange(Number(event.target.value))}
        />
      </Box>
    </Flexbox>
  );
};

export default AttributeRow;
