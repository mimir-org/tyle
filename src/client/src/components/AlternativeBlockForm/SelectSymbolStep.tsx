import { Box, Button, Flexbox } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetSymbols } from "api/symbol.queries";
import EngineeringSymbolSvg from "components/EngineeringSymbolSvg";
import Loader from "components/Loader";
import React from "react";
import { useTheme } from "styled-components";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import SymbolCard from "./SymbolCard";

interface SelectSymbolStepProps {
  symbol: EngineeringSymbol | null;
  setSymbol: (nextSymbol: EngineeringSymbol | null) => void;
}

const SelectSymbolStep = ({ symbol, setSymbol }: SelectSymbolStepProps) => {
  const theme = useTheme();
  const symbolQuery = useGetSymbols();

  const [selectedSymbol, setSelectedSymbol] = React.useState<EngineeringSymbol | null>(null);

  return (
    <>
      {symbolQuery.isLoading && <Loader />}
      {symbolQuery.isSuccess && symbol === null && (
        <Flexbox flexDirection="column" gap={theme.mimirorg.spacing.xxxl}>
          <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl} flexWrap="wrap">
            {symbolQuery.data.map((symbol) => (
              <SymbolCard
                key={symbol.id}
                symbol={symbol}
                selected={selectedSymbol === symbol}
                onClick={() => setSelectedSymbol(symbol)}
              />
            ))}
          </Flexbox>
          <Box width="100%" style={{ textAlign: "right" }}>
            <Button onClick={() => setSymbol(selectedSymbol)}>Select symbol</Button>
          </Box>
        </Flexbox>
      )}
      {symbol != null && (
        <>
          <Box width="100%" maxWidth="500px" style={{ margin: "0 auto", position: "relative" }}>
            <Box width="fit-content" style={{ position: "absolute", top: 0, right: 0, cursor: "pointer" }}>
              <XCircle
                size="2rem"
                color={theme.mimirorg.color.dangerousAction.on}
                onClick={() => {
                  setSelectedSymbol(null);
                  setSymbol(null);
                }}
              />
            </Box>
            <EngineeringSymbolSvg symbol={symbol} fillContainer={true} showConnectionPoints={true} />
          </Box>
        </>
      )}
    </>
  );
};

export default SelectSymbolStep;
