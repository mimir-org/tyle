import { useGetSymbols } from "api/symbol.queries";
import Loader from "components/Loader";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import { SymbolListWrapper } from "./SelectSymbolStep.styled";
import SymbolCard from "./SymbolCard";

interface SelectSymbolStepProps {
  setSymbol: (nextSymbol: EngineeringSymbol | null) => void;
}

const SelectSymbolStep = ({ setSymbol }: SelectSymbolStepProps) => {
  const symbolQuery = useGetSymbols();

  return (
    <>
      {symbolQuery.isLoading && <Loader />}
      {symbolQuery.isSuccess && (
        <SymbolListWrapper>
          {symbolQuery.data.map((symbol) => (
            <SymbolCard key={symbol.id} symbol={symbol} onClick={() => setSymbol(symbol)} />
          ))}
        </SymbolListWrapper>
      )}
    </>
  );
};

export default SelectSymbolStep;
