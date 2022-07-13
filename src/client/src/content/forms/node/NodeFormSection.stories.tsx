import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Button } from "../../../complib/buttons";
import { Flexbox } from "../../../complib/layouts";
import { AttributeInfoButtonProps } from "../../common/attribute/AttributeInfoButton";
import { Default as AttributeInfoButton } from "../../common/attribute/AttributeInfoButton.stories";
import { NodeFormSection } from "./NodeFormSection";

export default {
  title: "Content/Forms/Node/NodeFormSection",
  component: NodeFormSection,
} as ComponentMeta<typeof NodeFormSection>;

const Template: ComponentStory<typeof NodeFormSection> = (args) => <NodeFormSection {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Section",
  children: "Simple text content in section",
};

export const WithAction = Template.bind({});
WithAction.args = {
  ...Default.args,
  action: <Button onClick={() => alert("[STORYBOOK] NodeFormSection.Action")}>Action</Button>,
};

export const WithCustomContent = Template.bind({});
WithCustomContent.args = {
  ...WithAction.args,
  children: (
    <Flexbox flexWrap={"wrap"} gap={"16px"}>
      {[...Array(7)].map((_, i) => (
        <AttributeInfoButton key={i} {...(AttributeInfoButton.args as AttributeInfoButtonProps)} />
      ))}
    </Flexbox>
  ),
};
