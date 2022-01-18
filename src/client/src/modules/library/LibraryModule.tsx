import { TextResources } from "../../assets/text";
import { LibraryComponent } from "./index";
import { useState } from "react";
import { Icon } from "../../compLibrary/icon";
import { LibHeader, Module } from "./styled";
import { LibraryIcon } from "../../assets/icons/modules";


/**
 * Component for the type library (temporary POC).
 * @returns a module with a drop-down of Types and a search input.
 */
const LibraryModule = () => {
  const [searchString, setSearchString] = useState("");

    return (
        <Module>
            <LibHeader>
                <Icon size={24} src={LibraryIcon} alt=""/>
                <p>{TextResources.Module_Library}</p>
            </LibHeader>
            <LibraryComponent search={(text: string) => setSearchString(text)} searchString={searchString}/>
        </Module>
    );
};

export default LibraryModule;
