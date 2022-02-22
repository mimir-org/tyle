import { CollapseIcon, ExpandIcon } from "../../../../../../../assets/icons/chevron";
import { ValueHeader } from "../../../../styled";

interface Props {
  expandList: boolean;
  setExpandList: (expand: boolean) => void;
  isMultiSelect: boolean;
  getValues: () => Record<string, boolean>;
}

const LocationValueHeader = ({ expandList, setExpandList, isMultiSelect, getValues }: Props) => (
  <ValueHeader onClick={() => setExpandList(!expandList)} multiSelect={isMultiSelect}>
    <p>
      {Object.entries(getValues())
        .filter(([_key, value]) => value === true)
        .map(([key, _value]) => {
          return (
            <span key={key}>
              {key}
              {isMultiSelect ? ", " : null}
            </span>
          );
        })}
    </p>
    <img src={expandList ? ExpandIcon : CollapseIcon} alt="expand" onClick={() => setExpandList(!expandList)} className="icon" />
  </ValueHeader>
);

export default LocationValueHeader;
