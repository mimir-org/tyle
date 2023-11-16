import { Box, Flexbox, Select, Token } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import EngineeringSymbolSvg from "components/EngineeringSymbolSvg";
import React from "react";
import { useTheme } from "styled-components";
import { ConnectionPoint } from "types/blocks/connectionPoint";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import { Option } from "utils";
import { TerminalTypeReferenceField } from "./BlockForm.helpers";

interface ConnectTerminalsToSymbolStepProps {
  symbol: EngineeringSymbol;
  removeSymbol: () => void;
  terminals: TerminalTypeReferenceField[];
  setTerminals: (nextTerminals: TerminalTypeReferenceField[]) => void;
}

const ConnectTerminalsToSymbolStep = ({
  symbol,
  removeSymbol,
  terminals,
  setTerminals,
}: ConnectTerminalsToSymbolStepProps) => {
  const theme = useTheme();

  const [hoveredConnectionPoint, setHoveredConnectionPoint] = React.useState<number | null>(null);

  const getTerminalOptions = (connectionPointId: number) => {
    return terminals
      .filter((terminal) => !terminal.connectionPoint?.id || terminal.connectionPoint.id === connectionPointId)
      .map((terminal) => ({ value: terminal.id, label: `${terminal.terminalName} (${terminal.direction})` }));
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
    <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.multiple(6)}>
      <Box width="100%" maxWidth="500px" style={{ margin: "0 auto", position: "relative" }}>
        <Box width="fit-content" style={{ position: "absolute", top: 0, right: 0, cursor: "pointer" }}>
          <XCircle size="2rem" color={theme.mimirorg.color.dangerousAction.on} onClick={removeSymbol} />
        </Box>
        <EngineeringSymbolSvg
          symbol={symbol}
          fillContainer={true}
          showConnectionPoints={true}
          animateConnectionPoint={hoveredConnectionPoint ?? undefined}
        />
      </Box>
      <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.base} style={{ margin: "0 auto" }}>
        {symbol.connectionPoints.map((connectionPoint) => (
          <>
            <Token
              key={connectionPoint.id}
              onMouseEnter={() => setHoveredConnectionPoint(connectionPoint.id)}
              onMouseLeave={() => setHoveredConnectionPoint(null)}
              variant={"secondary"}
            >
              {connectionPoint.identifier}
            </Token>
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
          </>
        ))}
      </Flexbox>
    </Flexbox>
  );
};

export default ConnectTerminalsToSymbolStep;
