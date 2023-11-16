import { Flexbox } from "@mimirorg/component-library";
import { useGetSymbols } from "api/symbol.queries";
import Loader from "components/Loader";
import { useTheme } from "styled-components";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import SymbolCard from "./SymbolCard";

interface SelectSymbolStepProps {
  setSymbol: (nextSymbol: EngineeringSymbol | null) => void;
}

const SelectSymbolStep = ({ setSymbol }: SelectSymbolStepProps) => {
  const theme = useTheme();
  const symbolQuery = useGetSymbols();

  return (
    <>
      {symbolQuery.isLoading && <Loader />}
      {symbolQuery.isSuccess && (
        <Flexbox flexDirection="row" gap={theme.mimirorg.spacing.xl} flexWrap="wrap">
          {symbolQuery.data.map((symbol) => (
            <SymbolCard key={symbol.id} symbol={symbol} onClick={() => setSymbol(symbol)} />
          ))}
        </Flexbox>
      )}
    </>
  );
};

export default SelectSymbolStep;
