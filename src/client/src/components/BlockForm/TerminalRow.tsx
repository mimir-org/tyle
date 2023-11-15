import { Box, Checkbox, Flexbox, Input, Select, Token } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { Direction } from "types/terminals/direction";
import { Option } from "utils";
import { TerminalTypeReferenceField } from "./BlockForm.helpers";

interface TerminalRowProps {
  field: TerminalTypeReferenceField;
  remove: () => void;
  value: TerminalTypeReferenceField;
  onChange: (terminalTypeReference: TerminalTypeReferenceField) => void;
  directionOptions: Option<Direction>[];
}

const TerminalRow = ({ field, remove, value, onChange, directionOptions }: TerminalRowProps) => {
  const { t } = useTranslation("entities");

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
    <Flexbox alignItems={"center"}>
      <Box flex={1}>
        <Token
          variant={"secondary"}
          actionable
          actionIcon={<XCircle />}
          actionText={t("block.terminals.remove")}
          onAction={remove}
          dangerousAction
        >
          {field.terminalName}
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

      <Box>
        <Select
          placeholder={t("common.templates.select", { object: t("block.terminal.name").toLowerCase() })}
          options={directionOptions}
          value={direction}
          onChange={handleDirectionChange}
        />
      </Box>
    </Flexbox>
  );
};

export default TerminalRow;