import { BlobData, CreateLibraryType, TerminalType } from "../../../../models";
import { GetBlockColor, GetBlockHeight } from "./helpers";
import { InfoSymbolWrapper, InfoText, InfoWrapper, InputOutputTerminals, PreviewObjectBlock, Terminals } from "../../styled";
import { ConnectorIcon } from "../../../../assets/icons/connectors";
import { Symbol } from "../../../../compLibrary/symbol";

interface Props {
  createLibraryType: CreateLibraryType;
  rdsLabel?: string;
  inputTerminals?: TerminalType[];
  outputTerminals?: TerminalType[];
  symbol?: BlobData;
}
/**
 * Component to show an object block with input output terminals
 * @param param0
 * @returns the visual block in Type Preview Info
 */
export const ObjectBlock = ({ createLibraryType, rdsLabel, inputTerminals, outputTerminals, symbol }: Props) => {
  const aspect = createLibraryType?.aspect;
  const inputCount = inputTerminals?.length;
  const outputCount = outputTerminals?.length;

  const showTerminals = (input: boolean): JSX.Element[] => {
    const terminalsArray: JSX.Element[] = [];
    const inputOutputArray = input ? inputTerminals : outputTerminals;
    inputOutputArray?.forEach((t, index) => {
      terminalsArray.push(
        <span key={index}>
          <ConnectorIcon style={{ fill: t.color }} />
        </span>
      );
    });
    return terminalsArray;
  };

  return (
    <PreviewObjectBlock minHeight={GetBlockHeight(inputCount, outputCount)} color={GetBlockColor(aspect)}>
      <InputOutputTerminals>
        {inputTerminals && <Terminals input={true}>{showTerminals(true)}</Terminals>}
        {outputTerminals && <Terminals input={false}>{showTerminals(false)}</Terminals>}
      </InputOutputTerminals>
      <InfoWrapper height={"100%"}>
        <InfoText>{rdsLabel}</InfoText>
        <InfoText>{createLibraryType.name}</InfoText>
        {symbol && (
          <InfoSymbolWrapper>
            <Symbol base64={symbol.data} text={symbol.name} />
          </InfoSymbolWrapper>
        )}
      </InfoWrapper>
    </PreviewObjectBlock>
  );
};

export default ObjectBlock;
