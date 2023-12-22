import { Checkbox, Input, Select, Token } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useState } from "react";
import { Direction } from "types/terminals/direction";
import { Option } from "utils";
import { TerminalTypeReferenceField } from "./BlockForm.helpers";
import { TerminalRowWrapper, TokenWrapper } from "./TerminalRow.styled";

interface TerminalRowProps {
  field: TerminalTypeReferenceField;
  remove: () => void;
  value: TerminalTypeReferenceField;
  onChange: (terminalTypeReference: TerminalTypeReferenceField) => void;
  directionOptions: Option<Direction>[];
}

const TerminalRow = ({ field, remove, value, onChange, directionOptions }: TerminalRowProps) => {
  const [minCount, setMinCount] = useState(field.minCount);
  const [maxCount, setMaxCount] = useState(field.maxCount);
  const [direction, setDirection] = useState<Option<Direction>>(
    directionOptions.find((x) => x.value === field.direction) ?? directionOptions[0],
  );

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

  const handleDirectionChange = (nextDirection: Option<Direction> | null) => {
    if (nextDirection === null) return;

    setDirection(nextDirection);
    onChange({ ...value, direction: nextDirection.value });
  };

  return (
    <TerminalRowWrapper>
      <TokenWrapper>
        <Token
          variant={"secondary"}
          actionable
          actionIcon={<XCircle />}
          actionText="Remove terminal"
          onAction={remove}
          dangerousAction
        >
          {field.terminalName}
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
      <Select options={directionOptions} value={direction} onChange={handleDirectionChange} />
    </TerminalRowWrapper>
  );
};

export default TerminalRow;
