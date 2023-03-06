import { PaperClip } from "@styled-icons/heroicons-outline";
import React, { ForwardedRef, forwardRef, useEffect, useRef, useState } from "react";
import { Button } from "complib/buttons";
import { FileInputContainer } from "./components/FileInput.styled";
import { FileItemComponent } from "./components/FileItemComponent";
import { FileComponentContainer } from "./FileComponent.styled";

export interface FileInfo {
  id: number;
  fileName: string;
  fileSize: number;
  file: string | null;
  contentType: string;
  description: string;
}

interface Props {
  value: FileInfo[];
  onChange?: (files: FileInfo[]) => void;
  placeholder?: string;
  tooltip?: string;
}

/**
 * Create a unique GUID id
 */
export const createId = (): string => {
  let d = new Date().getTime();
  let d2 = (typeof performance !== "undefined" && performance.now && performance.now() * 1000) || 0;
  return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function (c) {
    let r = Math.random() * 16;
    if (d > 0) {
      r = (d + r) % 16 | 0;
      d = Math.floor(d / 16);
    } else {
      r = (d2 + r) % 16 | 0;
      d2 = Math.floor(d2 / 16);
    }
    return (c === "x" ? r : (r & 0x3) | 0x8).toString(16);
  });
};

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

/**
 * Create a unique number id based on unique string
 * @param from
 */
export const createNumberId = (negative = true) => {
  const input = createId();
  let hash = 0;
  const len = input.length;

  for (let i = 0; i < len; i++) {
    hash = (hash << 5) - hash + input.charCodeAt(i);
    hash |= 0;
  }

  if (negative && hash > 0) {
    return hash * -1;
  } else {
    return hash;
  }
};

export const FileComponent = forwardRef(
  ({ value, onChange, placeholder, tooltip }: Props, ref: ForwardedRef<HTMLDivElement>) => {
    const inputFile = useRef<HTMLInputElement | null>(null);
    const [filelist, setFiles] = useState<FileInfo[]>(value);

    useEffect(() => {
      if (onChange != null) onChange(filelist);
      // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [filelist]);

    const onFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
      event.stopPropagation();
      event.preventDefault();
      const files = event.currentTarget.files;
      if (files == null) return;

      const addedFiles = Array.from(files);
      const filesToBeAdded: FileInfo[] = [];

      addedFiles.forEach(async (file) => {
        const bytes = await toBase64(file);

        const newFile: FileInfo = {
          id: createNumberId(),
          fileName: file.name,
          fileSize: file.size,
          file: bytes != null ? bytes.toString() : null,
          contentType: file.type,
          description: "",
        };
        filesToBeAdded.push(newFile);
        const list = [...filelist, ...filesToBeAdded];
        setFiles(list);
      });
    };

    const onFileRemove = (id: number) => {
      const copy = filelist.filter((f) => f.id !== id);
      setFiles([...copy]);
    };

    const onDescriptionChange = (id: number, description: string) => {
      setFiles(
        filelist.map((x) => {
          if (x.id === id) {
            return { ...x, description: description };
          } else {
            return x;
          }
        })
      );
    };

    return (
      <FileComponentContainer ref={ref}>
        <FileInputContainer>
          <input
            type={"file"}
            onChange={onFileChange.bind(this)}
            multiple
            ref={inputFile}
            style={{ display: "none" }}
          />
          <Button icon={<PaperClip size={24} />} onClick={() => inputFile?.current?.click()}>
            Add attachment
          </Button>
        </FileInputContainer>
        {filelist &&
          filelist.map((info, index) => {
            return (
              <div key={index}>
                <FileItemComponent
                  fileInfo={info}
                  onRemove={onFileRemove}
                  onDescriptionChange={onDescriptionChange}
                  placeholder={placeholder}
                  tooltip={tooltip}
                />
              </div>
            );
          })}
      </FileComponentContainer>
    );
  }
);

FileComponent.displayName = "FileComponent";
