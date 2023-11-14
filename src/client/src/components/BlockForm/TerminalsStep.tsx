import { Flexbox } from "@mimirorg/component-library";
import { useGetTerminals } from "api/terminal.queries";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import { useTheme } from "styled-components";
import { Direction } from "types/terminals/direction";
import { getOptionsFromEnum } from "utils";
import { TerminalTypeReferenceField } from "./BlockForm.helpers";
import TerminalRow from "./TerminalRow";
import { onAddTerminals, resolveAvailableTerminals } from "./TerminalsStep.helpers";
import { prepareTerminals } from "./prepareTerminals";

interface TerminalsStepProps {
  chosenTerminals: TerminalTypeReferenceField[];
  setTerminals: (nextTerminals: TerminalTypeReferenceField[]) => void;
}

const TerminalsStep = ({ chosenTerminals, setTerminals }: TerminalsStepProps) => {
  const theme = useTheme();

  const terminalQuery = useGetTerminals();
  const terminals = prepareTerminals(terminalQuery.data) ?? [];
  const available = resolveAvailableTerminals(chosenTerminals, terminals);

  const availableDirectionOptions = (i: number) => {
    let availableDirections = getOptionsFromEnum<Direction>(Direction);

    if (chosenTerminals[i].terminalQualifier !== Direction.Bidirectional) {
      const option = availableDirections.find((x) => x.value === chosenTerminals[i].terminalQualifier);
      return option ? [option] : [];
    }

    chosenTerminals.forEach((chosenTerminal, j) => {
      if (i !== j && chosenTerminal.terminalId === chosenTerminals[i].terminalId) {
        availableDirections = availableDirections.filter((direction) => direction.value !== chosenTerminal.direction);
      }
    });

    return availableDirections;
  };

  const handleTerminalRowChange = (changedTerminal: TerminalTypeReferenceField, index: number) => {
    const nextTerminals = [...chosenTerminals];
    nextTerminals[index] = changedTerminal;

    setTerminals(nextTerminals);
  };

  const handleRemove = (index: number) => {
    const nextTerminals = [...chosenTerminals];
    nextTerminals.splice(index, 1);
    setTerminals(nextTerminals);
  };

  return (
    <FormSection
      title="Add terminals"
      action={
        <SelectItemDialog
          title="Select terminals"
          description="You can select one or more terminals"
          searchFieldText="Search"
          addItemsButtonText="Add terminals"
          openDialogButtonText="Open select terminals dialog"
          items={available}
          onAdd={(ids) => onAddTerminals(ids, chosenTerminals, terminals, setTerminals)}
        />
      }
    >
      <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.xl}>
        {chosenTerminals.map((terminalTypeReference, index) => {
          return (
            <TerminalRow
              key={terminalTypeReference.id}
              field={terminalTypeReference}
              remove={() => handleRemove(index)}
              value={terminalTypeReference}
              onChange={(nextTerminal: TerminalTypeReferenceField) => {
                handleTerminalRowChange(nextTerminal, index);
              }}
              directionOptions={availableDirectionOptions(index)}
            />
          );
        })}
      </Flexbox>
    </FormSection>
  );
};

export default TerminalsStep;
