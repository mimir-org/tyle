import { ChangeEvent } from "react";
import { TextResources } from "../../assets/text";
import { TextInput, TypeInfo, TypeNameInput } from "./styled";
import { Dropdown } from "./components/dropdown";
import { Aspect, BlobData, CreateLibraryType, LocationType, ObjectType, Purpose } from "../../models";
import { GetAspects, GetFilteredBlobData, GetLocationTypes, GetObjectTypes, GetPurposes, IsLocation } from "./helpers";
import { AspectKey, ObjectTypeKey } from "./types";
import Validation from "./components/validation/Validation";
import {
  IsAspectSelectionInvalid,
  IsLocationSelectionInvalid,
  IsObjectSelectionInvalid,
  IsPurposeSelectionInvalid,
  IsSymbolSelectionInvalid,
  IsTypeNameInvalid,
} from "./validators";

interface Props {
  createLibraryType: CreateLibraryType;
  icons: BlobData[];
  locationTypes: LocationType[];
  purposes: Purpose[];
  onChange: <K extends keyof CreateLibraryType>(key: K, value: CreateLibraryType[K]) => void;
  isValidationVisible: boolean;
}

const TypeEditorInputs = ({ onChange, createLibraryType, icons, locationTypes, purposes, isValidationVisible }: Props) => (
  <TypeInfo>
    <Validation
      minWidth={"15%"}
      visible={isValidationVisible && IsAspectSelectionInvalid(createLibraryType)}
      message={TextResources.TypeEditor_Error_Aspect}
    >
      <Dropdown
        label={TextResources.TypeEditor_Aspect}
        categories={GetAspects()}
        onChange={(data: AspectKey) => onChange("aspect", Aspect[data])}
        defaultValue={createLibraryType && createLibraryType.aspect?.toString()}
        placeholder={TextResources.TypeEditor_Aspect_Placeholder}
      />
    </Validation>

    {createLibraryType && !IsLocation(createLibraryType.aspect) && (
      <Validation
        minWidth={"15%"}
        visible={isValidationVisible && IsObjectSelectionInvalid(createLibraryType)}
        message={TextResources.TypeEditor_Error_Object_Type}
      >
        <Dropdown
          label={TextResources.TypeEditor_Object_Type}
          categories={GetObjectTypes(createLibraryType.aspect)}
          onChange={(data: ObjectTypeKey) => onChange("objectType", ObjectType[data])}
          defaultValue={createLibraryType && createLibraryType.objectType?.toString()}
          placeholder={TextResources.TypeEditor_Object_Placeholder}
        />
      </Validation>
    )}

    {createLibraryType && IsLocation(createLibraryType.aspect) && (
      <Validation
        minWidth={"15%"}
        visible={isValidationVisible && IsLocationSelectionInvalid(createLibraryType)}
        message={TextResources.TypeEditor_Error_Location_Type}
      >
        <Dropdown
          label={TextResources.TypeEditor_Location_Type}
          categories={GetLocationTypes(locationTypes)}
          hasCategory={true}
          onChange={(data: LocationType) => onChange("locationType", data.id)}
          defaultValue={createLibraryType && createLibraryType.locationType && createLibraryType.locationType.toString()}
          placeholder={TextResources.TypeEditor_Location_Placeholder}
        />
      </Validation>
    )}

    <Validation
      minWidth={"15%"}
      visible={isValidationVisible && IsPurposeSelectionInvalid(createLibraryType)}
      message={TextResources.TypeEditor_Error_Purpose}
    >
      <Dropdown
        label={TextResources.TypeEditor_Purpose}
        categories={GetPurposes(purposes)}
        onChange={(data: Purpose) => onChange("purpose", data.id)}
        defaultValue={createLibraryType && createLibraryType.purpose?.toString()}
        placeholder={TextResources.TypeEditor_Purpose_Placeholder}
      />
    </Validation>

    <Validation
      minWidth={"15%"}
      visible={isValidationVisible && IsTypeNameInvalid(createLibraryType)}
      message={TextResources.TypeEditor_Error_Name}
    >
      <TypeNameInput>
        <p className="label">{TextResources.TypeEditor_Type_Name}</p>
        <TextInput
          type="text"
          defaultValue={createLibraryType && createLibraryType.name}
          placeholder={TextResources.TypeEditor_Type_Placeholder}
          onChange={(e: ChangeEvent<HTMLInputElement>) => {
            onChange("name", e.target.value);
          }}
        />
      </TypeNameInput>
    </Validation>

    <Validation
      minWidth={"15%"}
      visible={isValidationVisible && IsSymbolSelectionInvalid(createLibraryType)}
      message={TextResources.TypeEditor_Error_Symbol}
    >
      <Dropdown
        label={TextResources.TypeEditor_Symbol}
        categories={GetFilteredBlobData(icons)}
        onChange={(data: BlobData) => onChange("symbolId", data.id)}
        placeholder={TextResources.TypeEditor_Symbol_Placeholder}
        defaultValue={createLibraryType && createLibraryType.symbolId}
      />
    </Validation>
  </TypeInfo>
);

export default TypeEditorInputs;
