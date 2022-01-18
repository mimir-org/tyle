import { BlobData, CreateLibraryType, Rds, TerminalType } from "../../../../models";
import { ObjectBlock } from "./ObjectBlock";
import { ListLabel, ListWrapper } from "../../../../compLibrary/list";
import { InfoText, InfoWrapper, PreviewArea } from "../../styled";
import { IsTransportOrInterface } from "./helpers";
import { TextResources } from "../../../../assets/text";
import { InterfaceIcon, TransportIcon } from "../../../../assets/icons/type";
import { IsFunction, IsInterface, IsLocation, IsObjectBlock, IsProduct, IsTransport } from "../../helpers";

interface Props {
  createLibraryType: CreateLibraryType;
  rds?: Rds;
  inputTerminals?: TerminalType[];
  outputTerminals?: TerminalType[];
  terminal?: TerminalType;
  symbol?: BlobData;
}
/**
 * Component to show Preview area with selected object type, type name, rds and symbol
 * @param param0
 * @returns the visual type preview area
 */
export const TypePreview = ({ createLibraryType, rds, terminal, inputTerminals, outputTerminals, symbol }: Props) => {
  const aspect = createLibraryType?.aspect;
  const objectType = createLibraryType?.objectType;
  const rdsLabel = rds ? rds.code + " - " + rds.name : undefined;

  const showObjectBlock = () => {
    if (
      (IsLocation(aspect) && createLibraryType?.locationType !== "") ||
      ((IsFunction(aspect) || IsProduct(aspect)) && IsObjectBlock(objectType))
    ) {
      return (
        <ObjectBlock
          createLibraryType={createLibraryType}
          rdsLabel={rdsLabel}
          inputTerminals={inputTerminals}
          outputTerminals={outputTerminals}
          symbol={symbol}
        />
      );
    }
    return null;
  };

  return (
    <ListWrapper flex={1.5}>
      <ListLabel preview={true}>{TextResources.TypeEditor_New_Type_Preview}</ListLabel>
      <PreviewArea>
        {showObjectBlock()}
        {IsTransportOrInterface(createLibraryType) && (
          <InfoWrapper>
            <InfoText>{rdsLabel}</InfoText>
            <InfoText>{createLibraryType?.name}</InfoText>
            {(IsFunction(aspect) || IsProduct(aspect)) && IsTransport(objectType) && (
              <TransportIcon style={{ stroke: terminal?.color, fill: terminal?.color }}/>
            )}
            {(IsFunction(aspect) || IsProduct(aspect)) && IsInterface(objectType) && (
              <InterfaceIcon style={{ stroke: terminal?.color, fill: terminal?.color }}/>
            )}
          </InfoWrapper>
        )}
      </PreviewArea>
    </ListWrapper>
  );
};

export default TypePreview;
