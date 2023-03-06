import { Tooltip } from "complib/data-display";
import { Box, Flexbox } from "complib/layouts";
import { Text } from "complib/text";
import { FileInfo } from "../FileComponent";
import { FileItemContainer } from "./FileItemComponent.styled";
import { Photograph, MinusCircle } from "@styled-icons/heroicons-outline";

interface Props {
  fileInfo?: FileInfo;
  onRemove: () => void;
  tooltip?: string;
}

export const FileItemComponent = ({ fileInfo, onRemove, tooltip }: Props) => {
  return (
    <>
      {fileInfo != null && fileInfo.file != null && (
        <FileItemContainer>
          <Flexbox alignContent="center" alignItems="center" flexDirection="row" justifyContent="space-between">
              <Box display="flex">
                {(fileInfo.contentType.startsWith("image")) ? <img src={fileInfo.file} style={{ maxWidth: "192px", maxHeight: "96px" }} /> : <Photograph size={48} />}
                <Tooltip content={tooltip ?? "Remove file"}>
                  <MinusCircle
                    className="fileitem-delete"
                    size={20}
                    color={"red"}
                    onClick={() => onRemove()}
                  />
                </Tooltip>
              </Box>
              <Text as="p" useEllipsis>
                {fileInfo.fileName}
              </Text>
              <Text as="p" useEllipsis>
                {fileInfo.fileSize} byte
              </Text>
          </Flexbox>
        </FileItemContainer>
      )}
    </>
  );
};
