import { EngineeringSymbol } from "types/blocks/engineeringSymbol";

interface EngineeringSymbolSvgProps {
  symbol: EngineeringSymbol;
  width?: number;
  height?: number;
}

const EngineeringSymbolSvg = ({ symbol, width = symbol.width, height = symbol.height }: EngineeringSymbolSvgProps) => {
  return (
    <svg
      xmlns="http://www.w3.org/2000/svg"
      width={width}
      height={height}
      viewBox={`0 0 ${symbol.width} ${symbol.height}`}
    >
      <path d={symbol.path} />
    </svg>
  );
};

export default EngineeringSymbolSvg;
