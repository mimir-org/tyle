import { EngineeringSymbol } from "types/blocks/engineeringSymbol";

interface EngineeringSymbolSvgProps {
  symbol: EngineeringSymbol;
  width?: number;
  height?: number;
  fillContainer?: boolean;
  showConnectionPoints?: boolean;
  animateConnectionPoint?: number;
}

const EngineeringSymbolSvg = ({
  symbol,
  width = symbol.width,
  height = symbol.height,
  fillContainer = false,
  showConnectionPoints = false,
  animateConnectionPoint,
}: EngineeringSymbolSvgProps) => {
  return (
    <svg
      xmlns="http://www.w3.org/2000/svg"
      width={width}
      height={height}
      viewBox={`0 0 ${symbol.width} ${symbol.height}`}
      style={{ width: fillContainer ? "100%" : undefined, height: fillContainer ? "auto" : undefined }}
    >
      <path d={symbol.path} />
      {showConnectionPoints &&
        symbol.connectionPoints.map((connectionPoint) => (
          <circle
            key={connectionPoint.identifier}
            cx={connectionPoint.positionX}
            cy={connectionPoint.positionY}
            r="2"
            fill="red"
          >
            {connectionPoint.id === animateConnectionPoint && (
              <animate attributeName="r" values="1;3;1" dur="1.5s" repeatCount="indefinite" />
            )}
          </circle>
        ))}
    </svg>
  );
};

export default EngineeringSymbolSvg;
