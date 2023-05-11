import { AspectObjectItem } from "../../../common/types/aspectObjectItem";
import { TerminalItem } from "../../../common/types/terminalItem";
import { AttributeItem } from "../../../common/types/attributeItem";
import { UnitItem } from "../../../common/types/unitItem";
import { DatumItem } from "../../../common/types/datumItem";
import { RdsItem } from "../../../common/types/rdsItem";

export type ItemType = AspectObjectItem | TerminalItem | AttributeItem | UnitItem | DatumItem | RdsItem;
