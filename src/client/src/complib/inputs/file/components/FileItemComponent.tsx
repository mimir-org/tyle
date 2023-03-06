import { Tooltip } from "complib/data-display";
import { Box, Flexbox } from "complib/layouts";
import { Textarea } from "complib/inputs/textarea/Textarea";
import { Text } from "complib/text";
import { FileInfo } from "../FileComponent";
import { FileItemContainer } from "./FileItemComponent.styled";
import { Photograph, MinusCircle } from "@styled-icons/heroicons-outline";

interface Props {
  fileInfo: FileInfo;
  onRemove: (id: number) => void;
  onDescriptionChange: (id: number, description: string) => void;
  placeholder?: string;
  tooltip?: string;
}

export const FileItemComponent = ({ fileInfo, onRemove, onDescriptionChange, placeholder, tooltip }: Props) => {
  return (
    <>
      {fileInfo != null && (
        <FileItemContainer>
          <Flexbox alignContent="center" alignItems="center" flexDirection="row" justifyContent="space-between">
            <Box width="25%">
              <Box display="flex">
                <Photograph size={48} />
                <Tooltip content={tooltip ?? "Remove file"}>
                  <MinusCircle
                    className="fileitem-delete"
                    size={20}
                    color={"red"}
                    onClick={() => onRemove(fileInfo.id)}
                  />
                </Tooltip>
              </Box>
              <Text as="h3" useEllipsis>
                {fileInfo.fileName}
              </Text>
              <Text as="h5" useEllipsis>
                {fileInfo.fileSize} byte
              </Text>
            </Box>
            <Box width="75%">
              <Textarea
                placeholder={placeholder ?? "Enter a file description here"}
                onChange={(data) => onDescriptionChange(fileInfo.id, data.target.value)}
                value={fileInfo.description}
              />
            </Box>
          </Flexbox>
        </FileItemContainer>
      )}
    </>
  );
};
