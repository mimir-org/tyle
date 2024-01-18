import { useGetTerminals } from "api/terminal.queries";
import FormSection from "components/FormSection";
import SelectItemDialog from "components/SelectItemDialog";
import React from "react";
import { Direction } from "types/terminals/direction";
import { getOptionsFromEnum } from "utils";
import { BlockFormStepProps } from "./BlockForm";
import { TerminalTypeReferenceField } from "./BlockForm.helpers";
import TerminalRow from "./TerminalRow";
import {
  mapTerminalViewsToInfoItems,
  onAddTerminals,
  prepareTerminals,
  resolveAvailableTerminals,
} from "./TerminalsForm.helpers";
import { TerminalsFormWrapper } from "./TerminalsForm.styled";

const TerminalsForm = React.forwardRef<HTMLFormElement, BlockFormStepProps>(({ fields, setFields }, ref) => {
  const { terminals } = fields;
  const setTerminals = (terminals: TerminalTypeReferenceField[]) => setFields({ ...fields, terminals });

  const terminalQuery = useGetTerminals();

  const availableTerminals = mapTerminalViewsToInfoItems(
    resolveAvailableTerminals(terminals, prepareTerminals(terminalQuery.data) ?? []),
  );

  const availableDirectionOptions = (i: number) => {
    let availableDirections = getOptionsFromEnum<Direction>(Direction);

    if (terminals[i].terminalQualifier !== Direction.Bidirectional) {
      const option = availableDirections.find((x) => x.value === terminals[i].terminalQualifier);
      return option ? [option] : [];
    }

    terminals.forEach((chosenTerminal, j) => {
      if (i !== j && chosenTerminal.terminalId === terminals[i].terminalId) {
        availableDirections = availableDirections.filter((direction) => direction.value !== chosenTerminal.direction);
      }
    });

    return availableDirections;
  };

  const handleTerminalRowChange = (changedTerminal: TerminalTypeReferenceField, index: number) => {
    const nextTerminals = [...terminals];
    nextTerminals[index] = changedTerminal;

    setTerminals(nextTerminals);
  };

  const handleRemove = (index: number) => {
    const nextTerminals = [...terminals];
    nextTerminals.splice(index, 1);
    setTerminals(nextTerminals);
  };

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  };

  return (
    <form onSubmit={handleSubmit} ref={ref}>
      <FormSection
        title="Add terminals"
        action={
          <SelectItemDialog
            title="Select terminals"
            description="You can select one or more terminals"
            searchFieldText="Search"
            addItemsButtonText="Add terminals"
            openDialogButtonText="Open select terminals dialog"
            items={availableTerminals}
            onAdd={(ids) => onAddTerminals(ids, terminals, terminalQuery.data ?? [], setTerminals)}
          />
        }
      >
        <TerminalsFormWrapper>
          {terminals.map((terminalTypeReference, index) => {
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
        </TerminalsFormWrapper>
      </FormSection>
    </form>
  );
});

TerminalsForm.displayName = "TerminalsForm";

export default TerminalsForm;
