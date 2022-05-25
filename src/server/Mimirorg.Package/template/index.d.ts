export enum Aspect
{
	NotSet = 0,
	None = 1,
	Function = 2,
	Product = 4,
	Location = 8,
}
export enum ConnectorDirection
{
	Input = 0,
	Output = 1,
	Bidirectional = 2,
}
export enum Select
{
	None = 0,
	SingleSelect = 1,
	MultiSelect = 2,
}
export enum Discipline
{
	None = 0,
	NotSet = 1,
	ProjectManagement = 2,
	Electrical = 4,
	Automation = 8,
	Structural = 16,
	Operation = 32,
	Process = 64,
}
export enum State
{
	Draft = 0,
	Deleted = 1,
	ApprovedCompany = 2,
	ApprovedGlobal = 3,
}
export interface UnitLibAm
{
	name: string;
	contentReferences: string[];
	description: string;
}
export interface TransportLibAm
{
	name: string;
	rdsName: string;
	rdsCode: string;
	purposeName: string;
	aspect: Aspect;
	companyId: number;
	terminalId: string;
	attributeIdList: string[];
	contentReferences: string[];
	description: string;
	parentId: string;
}
export interface TerminalLibAm
{
	name: string;
	parentId: string;
	contentReferences: string[];
	color: string;
	description: string;
	attributeIdList: string[];
}
export interface SymbolLibAm
{
	name: string;
	data: string;
	contentReferences: string[];
}
export interface SimpleLibAm
{
	name: string;
	contentReferences: string[];
	description: string;
	attributes: string[];
}
export interface SelectedAttributePredefinedLibAm
{
	key: string;
	isMultiSelect: boolean;
	values: { [index: string]: boolean };
	contentReferences: string[];
}
export interface RdsLibAm
{
	code: string;
	name: string;
	contentReferences: string[];
}
export interface PurposeLibAm
{
	name: string;
	contentReferences: string[];
	description: string;
}
export interface NodeTerminalLibAm
{
	terminalId: string;
	number: number;
	connectorDirection: ConnectorDirection;
}
export interface NodeLibAm
{
	name: string;
	rdsName: string;
	rdsCode: string;
	purposeName: string;
	aspect: Aspect;
	companyId: number;
	simpleIdList: string[];
	attributeIdList: string[];
	nodeTerminals: NodeTerminalLibAm[];
	selectedAttributePredefined: SelectedAttributePredefinedLibAm[];
	description: string;
	symbol: string;
	attributeAspectIri: string;
	contentReferences: string[];
	parentId: string;
}
export interface InterfaceLibAm
{
	name: string;
	rdsName: string;
	rdsCode: string;
	purposeName: string;
	aspect: Aspect;
	companyId: number;
	terminalId: string;
	attributeIdList: string[];
	contentReferences: string[];
	description: string;
	parentId: string;
}
export interface AttributeSourceLibAm
{
	name: string;
	contentReferences: string[];
	description: string;
}
export interface AttributeQualifierLibAm
{
	name: string;
	contentReferences: string[];
	description: string;
}
export interface AttributePredefinedLibAm
{
	key: string;
	isMultiSelect: boolean;
	valueStringList: string[];
	aspect: Aspect;
	contentReferences: string[];
}
export interface AttributeLibAm
{
	name: string;
	aspect: Aspect;
	discipline: Discipline;
	select: Select;
	attributeQualifier: string;
	attributeSource: string;
	attributeCondition: string;
	attributeFormat: string;
	contentReferences: string[];
	parentId: string;
	selectValues: string[];
	unitIdList: string[];
	tags: string[];
}
export interface AttributeFormatLibAm
{
	name: string;
	contentReferences: string[];
	description: string;
}
export interface AttributeConditionLibAm
{
	name: string;
	contentReferences: string[];
	description: string;
}
export interface AttributeAspectLibAm
{
	name: string;
	aspect: Aspect;
	contentReferences: string[];
	parentId: string;
	description: string;
}
export interface UnitLibCm
{
	id: string;
	name: string;
	iri: string;
	contentReferences: string[];
	description: string;
	created: Date;
	createdBy: string;
	kind: string;
}
export interface TransportLibCm
{
	id: string;
	parentName: string;
	parentIri: string;
	name: string;
	version: string;
	firstVersionId: string;
	iri: string;
	contentReferences: string[];
	rdsCode: string;
	rdsName: string;
	purposeName: string;
	aspect: Aspect;
	companyId: number;
	terminalId: string;
	terminal: TerminalLibCm;
	description: string;
	created: Date;
	createdBy: string;
	attributes: AttributeLibCm[];
	kind: string;
}
export interface TerminalLibCm
{
	id: string;
	parentName: string;
	parentIri: string;
	name: string;
	version: string;
	firstVersionId: string;
	iri: string;
	contentReferences: string[];
	color: string;
	description: string;
	created: Date;
	createdBy: string;
	attributes: AttributeLibCm[];
	children: TerminalLibCm[];
	kind: string;
}
export interface SymbolLibCm
{
	id: string;
	name: string;
	iri: string;
	contentReferences: string[];
	data: string;
	created: Date;
	createdBy: string;
	kind: string;
}
export interface SimpleLibCm
{
	id: string;
	name: string;
	iri: string;
	contentReferences: string[];
	description: string;
	created: Date;
	createdBy: string;
	attributes: AttributeLibCm[];
	kind: string;
}
export interface SelectedAttributePredefinedLibCm
{
	key: string;
	iri: string;
	contentReferences: string[];
	isMultiSelect: boolean;
	values: { [index: string]: boolean };
	aspect: Aspect;
	kind: string;
}
export interface RdsLibCm
{
	id: string;
	name: string;
	iri: string;
	contentReferences: string[];
	created: Date;
	createdBy: string;
	kind: string;
}
export interface PurposeLibCm
{
	id: string;
	name: string;
	iri: string;
	contentReferences: string[];
	description: string;
	created: Date;
	createdBy: string;
	kind: string;
}
export interface NodeTerminalLibCm
{
	id: string;
	number: number;
	connectorDirection: ConnectorDirection;
	terminal: TerminalLibCm;
	kind: string;
}
export interface NodeLibCm
{
	id: string;
	parentName: string;
	parentIri: string;
	name: string;
	version: string;
	firstVersionId: string;
	iri: string;
	attributeAspectIri: string;
	contentReferences: string[];
	rdsCode: string;
	rdsName: string;
	purposeName: string;
	aspect: Aspect;
	state: State;
	companyId: number;
	symbol: string;
	description: string;
	created: Date;
	createdBy: string;
	nodeTerminals: NodeTerminalLibCm[];
	attributes: AttributeLibCm[];
	simples: SimpleLibCm[];
	selectedAttributePredefined: SelectedAttributePredefinedLibCm[];
	kind: string;
}
export interface InterfaceLibCm
{
	id: string;
	parentName: string;
	parentIri: string;
	name: string;
	version: string;
	firstVersionId: string;
	iri: string;
	contentReferences: string[];
	rdsCode: string;
	rdsName: string;
	purposeName: string;
	aspect: Aspect;
	companyId: number;
	terminalId: string;
	terminal: TerminalLibCm;
	description: string;
	created: Date;
	createdBy: string;
	attributes: AttributeLibCm[];
	kind: string;
}
export interface AttributeSourceLibCm
{
	id: number;
	name: string;
	iri: string;
	contentReferences: string[];
	description: string;
	created: Date;
	createdBy: string;
	kind: string;
}
export interface AttributeQualifierLibCm
{
	id: number;
	name: string;
	iri: string;
	contentReferences: string[];
	description: string;
	created: Date;
	createdBy: string;
	kind: string;
}
export interface AttributePredefinedLibCm
{
	key: string;
	iri: string;
	contentReferences: string[];
	isMultiSelect: boolean;
	valueStringList: string[];
	aspect: Aspect;
	created: Date;
	createdBy: string;
	kind: string;
}
export interface AttributeLibCm
{
	id: string;
	parentName: string;
	parentIri: string;
	name: string;
	iri: string;
	contentReferences: string[];
	attributeQualifier: string;
	attributeSource: string;
	attributeCondition: string;
	attributeFormat: string;
	aspect: Aspect;
	discipline: Discipline;
	tags: string[];
	select: Select;
	selectValues: string[];
	units: UnitLibCm[];
	description: string;
	created: Date;
	createdBy: string;
	kind: string;
}
export interface AttributeFormatLibCm
{
	id: number;
	name: string;
	iri: string;
	contentReferences: string[];
	description: string;
	created: Date;
	createdBy: string;
	kind: string;
}
export interface AttributeConditionLibCm
{
	id: number;
	name: string;
	iri: string;
	contentReferences: string[];
	description: string;
	created: Date;
	createdBy: string;
	kind: string;
}
export interface AttributeAspectLibCm
{
	id: string;
	parentName: string;
	parentIri: string;
	name: string;
	iri: string;
	contentReferences: string[];
	aspect: Aspect;
	description: string;
	created: Date;
	createdBy: string;
	children: AttributeAspectLibCm[];
	kind: string;
}
