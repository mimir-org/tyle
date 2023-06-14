import { AspectObjectItem } from "../../../common/types/aspectObjectItem";
import { TerminalItem } from "../../../common/types/terminalItem";
import { AttributeItem } from "../../../common/types/attributeItem";
import { UnitItem } from "../../../common/types/unitItem";
import { QuantityDatumItem } from "../../../common/types/quantityDatumItem";
import { RdsItem } from "../../../common/types/rdsItem";

export type ItemType = AspectObjectItem | TerminalItem | AttributeItem | UnitItem | QuantityDatumItem | RdsItem;
