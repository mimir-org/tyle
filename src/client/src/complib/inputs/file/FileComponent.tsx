import { PaperClip } from "@styled-icons/heroicons-outline";
import React, { ForwardedRef, forwardRef, useEffect, useRef, useState } from "react";
import { Button } from "complib/buttons";
import { FileInputContainer } from "./components/FileInput.styled";
import { FileItemComponent } from "./components/FileItemComponent";
import { FileComponentContainer } from "./FileComponent.styled";
import { toast } from "complib/data-display";

export interface FileInfo {
  fileName: string;
  fileSize: number;
  file: string | null;
  contentType: string;
}

interface Props {
  accept?: string;
  onChange?: (file: FileInfo) => void;
  tooltip?: string;
}

/**
 * Read data from file and create a base64 string
 *
 * @param file  the file that should be converted
 */
export const toBase64 = (file: File) =>
  new Promise<string | ArrayBuffer | null>((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result);
    reader.onerror = (error) => reject(error);
  });

export const FileComponent = forwardRef(
  ({ accept, onChange, tooltip }: Props, ref: ForwardedRef<HTMLDivElement>) => {
    const inputFile = useRef<HTMLInputElement | null>(null);
    const [file, setFile] = useState<FileInfo>({
      fileName: "",
      fileSize: 0,
      file: null,
      contentType: ""
    });

    useEffect(() => {
      if (onChange != null) onChange(file);
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [file]);

    const onFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
      event.stopPropagation();
      event.preventDefault();
      const files = event.currentTarget.files;
      if (files == null) return;

      const addedFile = files[0];

      encodeAndSetFile(addedFile);
    };

    const encodeAndSetFile = async (addedFile: File) => {
      if (accept) {
        const acceptArray = accept.split(",");
        const contentType = addedFile.type;
        const filenameExtension = addedFile.name.split(".").length > 1 ? addedFile.name.split(".").slice(-1) : "";

        let correctFiletype = false;
        acceptArray.forEach(x => {
          if (x.startsWith(".") && x == `.${filenameExtension}`) correctFiletype = true;
          else if (x == contentType) correctFiletype = true;
        });

        if (!correctFiletype) {
          toast.error(`Incorrect filetype: ${contentType}`);
          return;
        }
      }

      const bytes = await toBase64(addedFile);
      const fileToBeAdded: FileInfo = {
        fileName: addedFile.name,
        fileSize: addedFile.size,
        file: bytes != null ? bytes.toString(): null,
        contentType: addedFile.type,
      };
      
      setFile(fileToBeAdded);
    }

    const onFileRemove = () => {
      setFile({
        fileName: "",
        fileSize: 0,
        file: null,
        contentType: ""
      });
      
      if (inputFile.current != null)
        inputFile.current.value = "";
    };

    const acceptedFiletypes = accept ? { accept: accept } : {}

    return (
      <FileComponentContainer ref={ref}>
        <FileInputContainer>
          <input
            { ...acceptedFiletypes }
            type={"file"}
            onChange={onFileChange.bind(this)}
            ref={inputFile}
            style={{ display: "none" }}
          />
          <Button icon={<PaperClip size={24} />} onClick={() => inputFile?.current?.click()}>
            Add attachment
          </Button>
        </FileInputContainer>
        {file && (
            <div>
              <FileItemComponent
                fileInfo={file}
                onRemove={onFileRemove}
                tooltip={tooltip}
              />
            </div>
          )
        }
      </FileComponentContainer>
    );
  }
);

FileComponent.displayName = "FileComponent";
