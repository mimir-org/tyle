import { XCircle } from "@styled-icons/heroicons-outline";
import EngineeringSymbolSvg from "components/EngineeringSymbolSvg";
import Select from "components/Select";
import React from "react";
import { ConnectionPoint } from "types/blocks/connectionPoint";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import { Direction } from "types/terminals/direction";
import { Option } from "utils";
import { TerminalTypeReferenceField } from "./BlockForm.helpers";
import {
  ConnectTerminalsWrapper,
  ConnectionPointList,
  ConnectionPointListItem,
  RemoveSymbolIconWrapper,
  SymbolPreview,
} from "./ConnectTerminalsToSymbolStep.styled";

interface ConnectTerminalsToSymbolStepProps {
  symbol: EngineeringSymbol;
  setSymbol: (symbol: EngineeringSymbol | null) => void;
  terminals: TerminalTypeReferenceField[];
  setTerminals: (terminals: TerminalTypeReferenceField[]) => void;
}

const ConnectTerminalsToSymbolStep = ({
  symbol,
  setSymbol,
  terminals,
  setTerminals,
}: ConnectTerminalsToSymbolStepProps) => {
  const [hoveredConnectionPoint, setHoveredConnectionPoint] = React.useState<number | null>(null);

  const getTerminalOptions = (connectionPointId: number) => {
    return terminals
      .filter((terminal) => !terminal.connectionPoint?.id || terminal.connectionPoint.id === connectionPointId)
      .map((terminal) => ({
        value: terminal.id,
        label: `${terminal.terminalName} (${Direction[terminal.direction]})`,
      }));
  };

  const handleRemoveSymbol = () => {
    const nextTerminals = terminals.map((terminal) => ({
      ...terminal,
      connectionPoint: null,
    }));

    setTerminals(nextTerminals);
    setSymbol(null);
  };

  const handleTerminalChange = (nextTerminal: Option<string> | null, connectionPoint: ConnectionPoint) => {
    const nextTerminals = [...terminals];

    const currentAssociatedTerminalIndex = nextTerminals.findIndex((x) => x.connectionPoint?.id === connectionPoint.id);
    if (currentAssociatedTerminalIndex >= 0) nextTerminals[currentAssociatedTerminalIndex].connectionPoint = null;

    if (nextTerminal?.value) {
      const nextAssociatedTerminalIndex = nextTerminals.findIndex((x) => x.id === nextTerminal.value);
      if (nextAssociatedTerminalIndex >= 0)
        nextTerminals[nextAssociatedTerminalIndex].connectionPoint = connectionPoint;
    }

    setTerminals(nextTerminals);
  };

  return (
    <ConnectTerminalsWrapper>
      <SymbolPreview>
        <RemoveSymbolIconWrapper>
          <XCircle size="2rem" onClick={handleRemoveSymbol} />
        </RemoveSymbolIconWrapper>
        <EngineeringSymbolSvg
          symbol={symbol}
          fillContainer={true}
          showConnectionPoints={true}
          animateConnectionPoint={hoveredConnectionPoint ?? undefined}
        />
      </SymbolPreview>
      <ConnectionPointList>
        {symbol.connectionPoints.map((connectionPoint, index) => (
          <ConnectionPointListItem
            key={connectionPoint.identifier}
            onMouseEnter={() => setHoveredConnectionPoint(connectionPoint.id)}
            onMouseLeave={() => setHoveredConnectionPoint(null)}
          >
            <p>Connection point #{index + 1}</p>
            <Select
              placeholder={"Select a terminal"}
              options={getTerminalOptions(connectionPoint.id)}
              value={getTerminalOptions(connectionPoint.id).find(
                (option) =>
                  option.value ===
                  terminals.find((terminal) => terminal.connectionPoint?.id === connectionPoint.id)?.id,
              )}
              onChange={(terminal) => handleTerminalChange(terminal, connectionPoint)}
              isClearable={true}
            />
          </ConnectionPointListItem>
        ))}
      </ConnectionPointList>
    </ConnectTerminalsWrapper>
  );
};

export default ConnectTerminalsToSymbolStep;
