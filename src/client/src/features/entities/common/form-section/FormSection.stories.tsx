import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Button } from "complib/buttons";
import { Flexbox } from "complib/layouts";
import { InfoItemButtonProps } from "../../../../common/components/info-item/InfoItemButton";
import { Default as SelectItemInfoButton } from "../../../../common/components/info-item/InfoItemButton.stories";
import { FormSection } from "./FormSection";

export default {
  title: "Entities/Common/FormSection",
  component: FormSection,
} as ComponentMeta<typeof FormSection>;

const Template: ComponentStory<typeof FormSection> = (args) => <FormSection {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Section",
  children: "Simple text content in section",
};

export const WithAction = Template.bind({});
WithAction.args = {
  ...Default.args,
  action: <Button onClick={() => alert("[STORYBOOK] FormSection.Action")}>Action</Button>,
};

export const WithCustomContent = Template.bind({});
WithCustomContent.args = {
  ...WithAction.args,
  children: (
    <Flexbox flexWrap={"wrap"} gap={"16px"}>
      {[...Array(7)].map((_, i) => (
        <SelectItemInfoButton key={i} {...(SelectItemInfoButton.args as InfoItemButtonProps)} />
      ))}
    </Flexbox>
  ),
};
