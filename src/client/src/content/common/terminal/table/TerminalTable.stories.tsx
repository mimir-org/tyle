import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockNodeTerminalItem } from "../../../../utils/mocks";
import { TerminalTable } from "./TerminalTable";

export default {
  title: "Content/Common/Terminal/TerminalTable",
  component: TerminalTable,
  args: {
    terminals: [...Array(7)].map((_) => mockNodeTerminalItem()),
  },
} as ComponentMeta<typeof TerminalTable>;

const Template: ComponentStory<typeof TerminalTable> = (args) => <TerminalTable {...args} />;

export const Default = Template.bind({});
