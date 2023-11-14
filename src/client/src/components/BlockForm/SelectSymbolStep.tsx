import { Box, Flexbox } from "@mimirorg/component-library";
import { XCircle } from "@styled-icons/heroicons-outline";
import { useGetSymbols } from "api/symbol.queries";
import EngineeringSymbolSvg from "components/EngineeringSymbolSvg";
import Loader from "components/Loader";
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

  return (
    <>
      {symbolQuery.isLoading && <Loader />}
      {symbolQuery.isSuccess && symbol === null && (
        <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl} flexWrap="wrap">
          {symbolQuery.data.map((symbol) => (
            <SymbolCard key={symbol.id} symbol={symbol} onClick={() => setSymbol(symbol)} />
          ))}
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
